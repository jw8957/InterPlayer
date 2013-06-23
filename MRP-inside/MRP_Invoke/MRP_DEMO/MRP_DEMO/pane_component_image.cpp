#include "pane_component_image.h"



int free_pane_component_image(Pane_Component_Image* pane_component)
{
	if(pane_component->pic != NULL)
		free(pane_component->pic);
	return 0;
}

int adjust_Pane_component_Image(Pane_Component_Image* pane_component,unsigned char* buf, int* offset_ptr,int length)
{
	//初始化
	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;
	pane_component->mime_type = 0;
	pane_component->href_flag = 0;
	pane_component->length_of_pic = 0;
	pane_component->pic = NULL;

	//结构体赋值

	pane_component->component_type = 1;
	if(length < (*offset_ptr) + 14)
		return -1;
	pane_component->pos_left = buf[(*offset_ptr)++] << 8;
	pane_component->pos_left += buf[(*offset_ptr)++];

	pane_component->pos_top = buf[(*offset_ptr)++] << 8;
	pane_component->pos_top += buf[(*offset_ptr)++];

	pane_component->pos_right = buf[(*offset_ptr)++] << 8;
	pane_component->pos_right += buf[(*offset_ptr)++];

	pane_component->pos_bottom = buf[(*offset_ptr)++] << 8;
	pane_component->pos_bottom += buf[(*offset_ptr)++];
	//printf("left %d \n",pane_component->pos_left);
	//printf("top %d \n",pane_component->pos_top);

	//printf("right %d \n",pane_component->pos_right);
	//printf("bottom %d \n",pane_component->pos_bottom);
	pane_component->mime_type = buf[(*offset_ptr)++];

	pane_component->href_flag = buf[(*offset_ptr)++];

	pane_component->length_of_pic = buf[(*offset_ptr)++] << 24;
	pane_component->length_of_pic += buf[(*offset_ptr)++] << 16;
	pane_component->length_of_pic += buf[(*offset_ptr)++] << 8;
	pane_component->length_of_pic += buf[(*offset_ptr)++];
	//printf("length_of_pic %lld \n",pane_component->length_of_pic);

	if(length < (*offset_ptr) + pane_component->length_of_pic)
		return -1;
	pane_component->pic = (unsigned char *)malloc(sizeof(unsigned char) * pane_component->length_of_pic);
	memcpy(pane_component->pic,buf + (*offset_ptr), pane_component->length_of_pic);
	(*offset_ptr) +=pane_component->length_of_pic;
	return 0;
}

void print_pane_component_Image(FILE* fp, Pane_Component_Image* pcI){
	fprintf(fp,"%d\n",pcI->component_type);
	fprintf(fp,"%d\n",pcI->unknown);
	fprintf(fp,"%d\n",pcI->pos_left);
	fprintf(fp,"%d\n",pcI->pos_top);
	fprintf(fp,"%d\n",pcI->pos_right);
	fprintf(fp,"%d\n",pcI->pos_bottom);
	fprintf(fp,"%d\n",pcI->mime_type);
	fprintf(fp,"%d\n",pcI->href_flag);
	fprintf(fp,"%d\n",pcI->length_of_pic);
	fprintf(fp,"%d\n",pcI->pic);
}