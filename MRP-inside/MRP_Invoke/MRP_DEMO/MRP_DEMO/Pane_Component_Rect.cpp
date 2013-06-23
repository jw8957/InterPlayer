#include "Pane_Component_Rect.h"


int free_pane_component_rect(Pane_Component_Rect* pane_component)
{
	return 0;
}
int adjust_Pane_component_rect(Pane_Component_Rect* pane_component,unsigned char* buf, int* offset_ptr,int length)
{

	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;
	pane_component->nwidth = 0;
	pane_component->ntype = 0;
	pane_component->ncolor = 0;
	pane_component->nflag = 0;
	pane_component->bg_type = 0;
	pane_component->bg_color = 0;

	pane_component->component_type = 6;
	if(length < (*offset_ptr) + 10)
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
	pane_component->nwidth = buf[(*offset_ptr)++];
	//printf("nwidth %d \n",pane_component->nwidth);
	pane_component->ntype += buf[(*offset_ptr)++];
	//printf("ntype %d \n",pane_component->ntype);
	if(pane_component->ntype == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		pane_component->ncolor = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
	    pane_component->ncolor = buf[(*offset_ptr)++] << 24;
	    pane_component->ncolor += buf[(*offset_ptr)++] << 16;
	    pane_component->ncolor += buf[(*offset_ptr)++] << 8;
	    pane_component->ncolor += buf[(*offset_ptr)++];
	}
	if(length < (*offset_ptr) + 1)
		return -1;
	pane_component->nflag = buf[(*offset_ptr)++];

	if(pane_component->nflag == 0)
	{
		pane_component->bg_type = 0;
		pane_component->bg_color = 0;
	}
	else
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		pane_component->bg_type = buf[(*offset_ptr)++];
		if(pane_component->bg_type == 0)
		{
			if(length < (*offset_ptr) + 1)
				return -1;
			pane_component->bg_color = buf[(*offset_ptr)++];
		}
		else
		{
			if(length < (*offset_ptr) + 4)
				return -1;
		    pane_component->bg_color = buf[(*offset_ptr)++] << 24;
	        pane_component->bg_color += buf[(*offset_ptr)++] << 16;
	        pane_component->bg_color += buf[(*offset_ptr)++] << 8;
	        pane_component->bg_color += buf[(*offset_ptr)++];
		}

	}
	return 0;
}

void print_pane_component_rect(FILE* fp, Pane_Component_Rect* pcR){
	fprintf(fp,"%d\n",pcR->component_type);
	fprintf(fp,"%d\n",pcR->unknown);
	fprintf(fp,"%d\n",pcR->pos_left);
	fprintf(fp,"%d\n",pcR->pos_top);
	fprintf(fp,"%d\n",pcR->pos_right);
	fprintf(fp,"%d\n",pcR->pos_bottom);
	fprintf(fp,"%d\n",pcR->nwidth);
	fprintf(fp,"%d\n",pcR->ntype);
	fprintf(fp,"%d\n",pcR->ncolor);
	fprintf(fp,"%d\n",pcR->nflag);
	fprintf(fp,"%d\n",pcR->bg_type);
	fprintf(fp,"%d\n",pcR->bg_color);
}