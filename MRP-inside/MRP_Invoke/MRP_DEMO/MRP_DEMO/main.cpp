#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <iostream>
#include <list>
#include "ts.h"
#include "pes.h"
#include "scene_show_section.h"
#include "scene_section.h"
#include "SYN_INF_STRUCT.h"

using namespace std;

#define MAX 100

list<unsigned char *> pes_packages;
list<unsigned char *> private_datas;
list<SYN_INF_STRUCT *> syn_inf_struct_list;
int private_datas_length[3000] = {0};
int pes_packages_length[5000] = {0};
char filename[MAX];

void ts_to_pes()
{
	int count=0;
	unsigned char buf[4];
    unsigned char data[184];
    unsigned char *pes_package;
    unsigned char *init;
    int rc = 0;

	FILE *infile;
    TS_packet_header *ts_header = (TS_packet_header *)malloc(sizeof(TS_packet_header));
    //infile = fopen("D:\\课程\\创新项目\\MRP\\6.TS","rb");
	//infile = fopen("D:\\InterPlayer\\TS_SSML2.TS","rb");
	infile = fopen(filename,"rb");
	//D:\\InterPlayer\\广告_new.TS
	while((rc = fread(buf,sizeof(unsigned char),4,infile))!=0)
	{

		adjust_TS_packet_header(ts_header,buf);
		rc = fread(data,sizeof(unsigned char),184,infile);

		if(ts_header->PID == 103)
		{
			if(ts_header->payload_unit_start_indicator == 1)
			{
				count++;
				pes_package = (unsigned char*)malloc(sizeof(unsigned char)*5000);
				init = pes_package;
				unsigned char *header = data + 1; 
				memcpy(init, header, 183);
				init +=183;
				pes_packages.push_back(pes_package);
				pes_packages_length[count - 1] +=183;
			}
			else if(ts_header->payload_unit_start_indicator == 0)
			{
				memcpy(init, data, 184);
				init +=184;
				pes_packages_length[count - 1] +=184;
			}
		}
	}
	
	fclose(infile);
	free(ts_header);
}

void pes_to_private_data()
{
	unsigned char pes_buf[5];
	PES_packet_header *pes_header = (PES_packet_header *)malloc(sizeof(PES_packet_header));

	unsigned char section_show_buf[2];
	Scene_show_section *scene_show_section = (Scene_show_section *)malloc(sizeof(Scene_show_section));

	unsigned char section_buf[14];
	Scene_section *scene_section = (Scene_section *)malloc(sizeof(Scene_section));

	int kk = 0;
	int pes_index = 0;
	int private_data_count = 0;
    unsigned char *private_data;
    unsigned char *private_init;


	for(list<unsigned char*>::iterator iterator = pes_packages.begin();iterator != pes_packages.end();++iterator)
	{
		memcpy(pes_buf, *iterator, 5);
		adjust_PES_packet_header(pes_header, pes_buf);
		//检测所有场景包内容状态
		if(pes_header->table_id == 0x88 && pes_header->table_extension_id != 0xfffe)
		{
			//kk++;
			unsigned char *scene = *iterator + 6;
			memcpy(section_buf, scene, 14);
			adjust_Scene_section(scene_section, section_buf);
			//过滤，只处理将要传送的场景长度不大于实际长度的包
			if(scene_section->current_trans_data_length <= pes_packages_length[pes_index] - 24)
			{
				kk++;
				//printf("%d\n",pes_index);
				if(scene_section->section_number == 0)
				{
					private_data_count++;
					private_data = (unsigned char*)malloc(sizeof(unsigned char) * scene_section->data_length);
					private_init = private_data;
					unsigned char *pes_data_content = *iterator + 20;
					private_datas_length[private_data_count - 1] +=scene_section->current_trans_data_length; 
					if(private_datas_length[private_data_count - 1] > scene_section->data_length)
					{
						free(private_data);
						continue;
					}
					memcpy(private_init, pes_data_content, scene_section->current_trans_data_length);
					private_init +=scene_section->current_trans_data_length;
					private_datas.push_back(private_data);
				}
				else
				{
					unsigned char *pes_data_content = *iterator + 20;
					private_datas_length[private_data_count - 1] +=scene_section->current_trans_data_length; 
					if(private_datas_length[private_data_count - 1] > scene_section->data_length)
					{
					    free(private_data);
						private_datas.pop_back();
						continue;
					}
					memcpy(private_init, pes_data_content, scene_section->current_trans_data_length);
					private_init +=scene_section->current_trans_data_length;

				}
			}
		}
		//释放已经读取的内容
		free(*iterator);
		*iterator = NULL;
		pes_index++;
	}
	free(pes_header);
	free(scene_show_section);
	free(scene_section);
}

void get_syn_inf_struct()
{
	syn_inf_struct_list.assign(0, 0);
	unsigned char *syn_inf_buf;
	SYN_INF_STRUCT *syn_inf_struct;
	int syn_count = 0;
	int pre_time = -10;
	for(list<unsigned char*>::iterator iterator = private_datas.begin();iterator != private_datas.end();++iterator)
	{

	    syn_inf_struct = (SYN_INF_STRUCT *)malloc(sizeof(SYN_INF_STRUCT));
		syn_inf_buf = (unsigned char*)malloc(sizeof(unsigned char) * private_datas_length[syn_count]);
		memcpy(syn_inf_buf, *iterator, private_datas_length[syn_count]);
		adjust_SYN_INF_STRUCT(syn_inf_struct, syn_inf_buf, private_datas_length[syn_count]);
		free(syn_inf_buf);
		syn_inf_buf = NULL;
		
		
		if(syn_inf_struct->ntime != pre_time)
		{
			pre_time = syn_inf_struct->ntime;
			//printf("ntime:%d\n",syn_inf_struct->ntime);
			syn_inf_struct_list.push_back(syn_inf_struct);
		}
		else{
			free_syn_inf_struct(syn_inf_struct);
			free(syn_inf_struct);
			syn_inf_struct = NULL;
		}
		free(*iterator);
		*iterator = NULL;
		syn_count++;
	}
}

void WirteToFile( FILE *fp ){
	fprintf( fp,"%d\n",syn_inf_struct_list.size() );
	for( list<SYN_INF_STRUCT *>::iterator it=syn_inf_struct_list.begin(); it!=syn_inf_struct_list.end(); ++it){
		print_SYN_INF_STRUCT(fp,*it);
	}
}

void replace(){
	char *ptr1,*ptr2;
	char result[MAX];
	memset(result,0,sizeof(result));

	ptr1=filename;
	ptr2=result;

	while( (*ptr1) >0){
		if(*ptr1!='\\')
			++ptr2;
		else{
			*ptr2=*ptr1;
			++ptr2;
			*ptr2=*ptr1;
		}
		++ptr1;
		++ptr2;
	}
	memcpy(filename,result,sizeof(result));
}

int main(int argc,char*argv[]){

	//printf("%s\n",argv[1]);
	//全局变量初始化
	pes_packages.assign(0,0);
    private_datas.assign(0,0);

	memset(filename,0,sizeof(filename));
	//memcpy(filename,argv[1],sizeof(argv[1]));
	strcpy(filename,argv[1]);
	//scanf("%s",filename);
	//replace();
	printf("%s\n",filename);

	ts_to_pes();
	pes_to_private_data();
	get_syn_inf_struct();

	FILE *outfile;	
    outfile = fopen("tmp.txt","w");

	WirteToFile(outfile);
	pes_packages.clear();
	private_datas.clear();
	fclose(outfile);
	//system("pause");
	return 0;
}