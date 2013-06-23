#include "pane_component_text.h"

int free_pane_component_text(Pane_Component_Text* pane_component)
{
	if(pane_component->text != NULL)
		free(pane_component->text);
	if(pane_component->info != NULL)
		free(pane_component->info);
	return 0;
}

int adjust_Pane_component_text(Pane_Component_Text* pane_component,unsigned char* buf, int* offset_ptr, int length)
{
	//先对结构体初始化,所以中途退出的话，不会出现崩溃和指针悬挂等情况
	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;
	pane_component->size = 0;
	pane_component->style = 0;
	pane_component->len = 0;
	pane_component->color1 = 0;
	pane_component->color2 = 0;
	pane_component->bg_color1 = 0;
	pane_component->bg_color2 = 0;
	pane_component->alpha = 0;
	pane_component->length_of_string = 0;
	pane_component->text = NULL;
	pane_component->href_flag = 0;
	pane_component->more_info = 0;
	pane_component->length_of_info = 0;
	pane_component->info = NULL;

	//开始赋值
	pane_component->component_type = 0;

	if(length < (*offset_ptr) + 14)
		return -1;

	pane_component->pos_left = buf[(*offset_ptr)++] << 8;
	pane_component->pos_left += buf[(*offset_ptr)++];
	//printf("left %d \n",pane_component->pos_left);
	pane_component->pos_top = buf[(*offset_ptr)++] << 8;
	pane_component->pos_top += buf[(*offset_ptr)++];
	//printf("top %d \n",pane_component->pos_top);
	pane_component->pos_right = buf[(*offset_ptr)++] << 8;
	pane_component->pos_right += buf[(*offset_ptr)++];
	//printf("right %d \n",pane_component->pos_right);
	pane_component->pos_bottom = buf[(*offset_ptr)++] << 8;
	pane_component->pos_bottom += buf[(*offset_ptr)++];
	//printf("bottom %d \n",pane_component->pos_bottom);
	pane_component->size = buf[(*offset_ptr)++];
	pane_component->style = buf[(*offset_ptr)++];

	pane_component->len = buf[(*offset_ptr)++] << 8;
	pane_component->len += buf[(*offset_ptr)++];

	pane_component->color1 = buf[(*offset_ptr)++];

	if(pane_component->color1 == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		pane_component->color2 = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
		pane_component->color2 = buf[(*offset_ptr)++] << 24;
		pane_component->color2 += buf[(*offset_ptr)++] << 16;
		pane_component->color2 += buf[(*offset_ptr)++] << 8;
		pane_component->color2 += buf[(*offset_ptr)++];
	}
	if(length < (*offset_ptr) + 1)
		return -1;
	pane_component->bg_color1 = buf[(*offset_ptr)++];

	if(pane_component->bg_color1 == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		pane_component->bg_color2 = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
		pane_component->bg_color2 = buf[(*offset_ptr)++] << 24;
		pane_component->bg_color2 += buf[(*offset_ptr)++] << 16;
		pane_component->bg_color2 += buf[(*offset_ptr)++] << 8;
		pane_component->bg_color2 += buf[(*offset_ptr)++];
	}
			
	if(length < (*offset_ptr) + 3)
		return -1;
	pane_component->alpha = buf[(*offset_ptr)++];

	pane_component->length_of_string = buf[(*offset_ptr)++] << 8;
	pane_component->length_of_string += buf[(*offset_ptr)++];


	//读取关键数据,字符串
	if(length < (*offset_ptr) + pane_component->length_of_string)
	{
		//printf("max_length %d need_length %d\n",length,pane_component->length_of_string);
		return -1;
	}
	
	pane_component->text = (unsigned char *)malloc(sizeof(unsigned char) * (pane_component->length_of_string + 1));
	memset(pane_component->text, '\0',sizeof(unsigned char) * (pane_component->length_of_string + 1));
	memcpy(pane_component->text,buf + (*offset_ptr), pane_component->length_of_string);
	//printf("text:%s\n",pane_component->text);
	(*offset_ptr) +=pane_component->length_of_string;

	if(length < (*offset_ptr) + 1)
		return -1;

	pane_component->href_flag = buf[(*offset_ptr)++];
	if(pane_component->href_flag == 1)
	{
		if(length < (*offset_ptr) + 2)
			return -1;
		pane_component->more_info = buf[(*offset_ptr)++] << 8;
		pane_component->more_info += buf[(*offset_ptr)++];

		pane_component->length_of_info = 0;
		pane_component->info = NULL;
	}
	else if(pane_component->href_flag == 2)
	{
		pane_component->more_info = 0;
		if(length < (*offset_ptr) + 2)
			return -1;
	    pane_component->length_of_info = buf[(*offset_ptr)++] << 8;
		pane_component->length_of_info += buf[(*offset_ptr)++];
		if(length < (*offset_ptr) + pane_component->length_of_info)
		{
			return -1;
		}
		pane_component->info = (unsigned char*)malloc(sizeof(unsigned char) * pane_component->length_of_info);
		memcpy(pane_component->info,buf + (*offset_ptr),pane_component->length_of_info);
		(*offset_ptr) +=pane_component->length_of_info;
	}
	else
	{
		pane_component->more_info = 0;
		pane_component->length_of_info = 0;
		pane_component->info = NULL;
	}
	return 0;
}

void print_pane_component_text( FILE* fp,Pane_Component_Text* pct ){
	fprintf(fp,"%d\n",pct->component_type);
	fprintf(fp,"%d\n",pct->unknown);
	fprintf(fp,"%d\n",pct->pos_left);
	fprintf(fp,"%d\n",pct->pos_top);
	fprintf(fp,"%d\n",pct->pos_right);
	fprintf(fp,"%d\n",pct->pos_bottom);
	fprintf(fp,"%d\n",pct->size);
	fprintf(fp,"%d\n",pct->style);
	fprintf(fp,"%d\n",pct->len);
	fprintf(fp,"%d\n",pct->color1);
	fprintf(fp,"%d\n",pct->color2);
	fprintf(fp,"%d\n",pct->bg_color1);
	fprintf(fp,"%d\n",pct->bg_color2);
	fprintf(fp,"%d\n",pct->alpha);
	fprintf(fp,"%d\n",pct->length_of_string);
	fprintf(fp,"%s\n",pct->text);
	fprintf(fp,"%d\n",pct->href_flag);
	fprintf(fp,"%d\n",pct->more_info);
	fprintf(fp,"%d\n",pct->length_of_info);
	fprintf(fp,"%s\n",pct->info);
}