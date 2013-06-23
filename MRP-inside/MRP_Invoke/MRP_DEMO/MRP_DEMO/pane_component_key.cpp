#include "pane_component_key.h"


int free_pane_component_key(Pane_Component_Key* pane_component)
{
	if(pane_component->info != NULL)
		free(pane_component->info);
	if(pane_component->return_info != NULL)
		free(pane_component->info);
	return 0;
}

int adjust_Pane_component_Key(Pane_Component_Key* pane_component,unsigned char* buf, int* offset_ptr, int length)
{
	//³õÊ¼»¯
	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->kcode = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;
	pane_component->href_flag = 0;
	pane_component->href_info = 0;
	pane_component->length_of_info = 0;
	pane_component->info = NULL;
	pane_component->return_flag = 0;
	pane_component->length_of_return_info = 0;
	pane_component->return_info = NULL;


	//¸³Öµ
	pane_component->component_type = 2;
	if(length < (*offset_ptr) + 10)
		return -1;
	pane_component->kcode = buf[(*offset_ptr)++];

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
	pane_component->href_flag = buf[(*offset_ptr)++];

    if(pane_component->href_flag == 1)
	{
		if(length < (*offset_ptr) + 2)
			return -1;
	    pane_component->href_info = buf[(*offset_ptr)++] << 8;
	    pane_component->href_info += buf[(*offset_ptr)++];
		pane_component->length_of_info = 0;
		pane_component->info = NULL;
	}
	else if(pane_component->href_flag == 2)
	{
		pane_component->href_info = 0;
		if(length < (*offset_ptr) + 2)
			return -1;
	    pane_component->length_of_info = buf[(*offset_ptr)++] << 8;
	    pane_component->length_of_info += buf[(*offset_ptr)++];
		if(length < (*offset_ptr) + pane_component->length_of_info)
			return -1;
		pane_component->info = (unsigned char *)malloc(sizeof(unsigned char) * pane_component->length_of_info);
	    memcpy(pane_component->info,buf + (*offset_ptr), pane_component->length_of_info);
	    (*offset_ptr) +=pane_component->length_of_info;
	}
	else
	{
		pane_component->href_info = 0;
		pane_component->length_of_info = 0;
		pane_component->info = NULL;
	}
	if(length < (*offset_ptr) + 1)
		return -1;
	pane_component->return_flag = buf[(*offset_ptr)++];
	if(pane_component->return_flag == 1)
	{
		if(length < (*offset_ptr) + 2)
			return -1;
	    pane_component->length_of_return_info = buf[(*offset_ptr)++] << 8;
	    pane_component->length_of_return_info += buf[(*offset_ptr)++];
		if(length < (*offset_ptr) + pane_component->length_of_return_info)
			return -1;
		pane_component->return_info = (unsigned char *)malloc(sizeof(unsigned char) * pane_component->length_of_return_info);
		memcpy(pane_component->return_info,buf + (*offset_ptr), pane_component->length_of_return_info);
	    (*offset_ptr) +=pane_component->length_of_return_info;
	}
	else
	{
		pane_component->length_of_return_info = 0;
		pane_component->return_info = NULL;
	}
	return 0;
}

void print_pane_component_Key(FILE* fp, Pane_Component_Key* pcK){
	fprintf(fp,"%d\n",pcK->component_type);
	fprintf(fp,"%d\n",pcK->unknown);
	fprintf(fp,"%d\n",pcK->kcode);
	fprintf(fp,"%d\n",pcK->pos_left);
	fprintf(fp,"%d\n",pcK->pos_top);
	fprintf(fp,"%d\n",pcK->pos_right);
	fprintf(fp,"%d\n",pcK->pos_bottom);
	fprintf(fp,"%d\n",pcK->href_flag);
	fprintf(fp,"%d\n",pcK->href_info);
	fprintf(fp,"%d\n",pcK->length_of_info);
	fprintf(fp,"%s\n",pcK->info);
	fprintf(fp,"%d\n",pcK->return_flag);
	fprintf(fp,"%d\n",pcK->length_of_return_info);
	fprintf(fp,"%s\n",pcK->return_info);
}