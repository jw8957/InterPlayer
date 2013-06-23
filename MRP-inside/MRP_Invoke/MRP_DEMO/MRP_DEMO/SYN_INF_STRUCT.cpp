#include "SYN_INF_STRUCT.h"


int free_syn_inf_struct(SYN_INF_STRUCT* syn_inf_struct)
{
	if(syn_inf_struct->panes != NULL)
	{
		for(int i = 0; i < syn_inf_struct->sub_pane_count; i++)
			free_panes(syn_inf_struct->panes, i);
	}
	free(syn_inf_struct->panes);
	return 0;
}


int adjust_SYN_INF_STRUCT(SYN_INF_STRUCT* syn_inf_struct,unsigned char* buf, int length)
{
	//读取所有的私有数据内容，分填充到对应的结构体中
	int static syn_counter = 0;
	int offset = 0;
	int* offset_ptr = &offset;

	if(length < (* offset_ptr) + 15)
		return -1;

	syn_inf_struct->ntime = buf[offset++] << 24;
	syn_inf_struct->ntime += buf[offset++] << 16;
	syn_inf_struct->ntime += buf[offset++] << 8;
	syn_inf_struct->ntime += buf[offset++];
	//printf("ntime %d \n",syn_inf_struct->ntime);

	syn_inf_struct->duration = buf[offset++] << 24;
	syn_inf_struct->duration += buf[offset++] << 16;
	syn_inf_struct->duration += buf[offset++] << 8;
	syn_inf_struct->duration += buf[offset++];
	//printf("duration %d \n",syn_inf_struct->duration);

	syn_inf_struct->main_pane_id = buf[offset++] << 8;
	syn_inf_struct->main_pane_id += buf[offset++];

	syn_inf_struct->sub_pane_count = buf[offset++];

	syn_inf_struct->reserved1 = buf[offset++] << 24;
	syn_inf_struct->reserved1 += buf[offset++] << 16;
	syn_inf_struct->reserved1 += buf[offset++] << 8;
	syn_inf_struct->reserved1 += buf[offset++];

	//生成大小合适的数组来容纳panes
	syn_inf_struct->panes = (PANE *)malloc(sizeof(PANE) * syn_inf_struct->sub_pane_count);

	//printf("\n%d\nsub_pane_count:%d\n",++syn_counter,syn_inf_struct->sub_pane_count);
	
		//开始填充pane
	for(int i = 0; i < syn_inf_struct->sub_pane_count; i++)
		adjust_Pane(syn_inf_struct->panes,i,buf,offset_ptr,length);
	
	//后期检查一下结尾，看看是否还有什么东西
	return 0;
}

void print_SYN_INF_STRUCT(FILE* fp,SYN_INF_STRUCT* syn_inf_struct){
	//FILE* fp;
	fprintf(fp,"%d\n",syn_inf_struct->ntime);
	fprintf(fp,"%d\n",syn_inf_struct->duration);
	fprintf(fp,"%d\n",syn_inf_struct->main_pane_id);
	fprintf(fp,"%d\n",syn_inf_struct->sub_pane_count);
	fprintf(fp,"%d\n",syn_inf_struct->reserved1);

	//fprintf();
	for(int i=0; i<syn_inf_struct->sub_pane_count; ++i){
		//fprintf(fp,"Pane:%d\n",i);
		print_Pane(fp,syn_inf_struct->panes[i]);
	}
}
