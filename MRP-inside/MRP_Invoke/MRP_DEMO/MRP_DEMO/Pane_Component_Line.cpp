#include "Pane_Component_Line.h"

int free_pane_component_line(Pane_Component_Line* pane_component)
{
	return 0;
}

int adjust_Pane_component_line(Pane_Component_Line* pane_component,unsigned char* buf, int* offset_ptr, int length)
{
	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;
	pane_component->width = 0;
	pane_component->ntype = 0;
	pane_component->color = 0;

	pane_component->component_type = 4;
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
	pane_component->width = buf[(*offset_ptr)++];
	//printf("nwidth %d \n",pane_component->width);
	pane_component->ntype += buf[(*offset_ptr)++];
	//printf("ntype %d \n",pane_component->ntype);
	if(pane_component->ntype == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		pane_component->color = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
	    pane_component->color = buf[(*offset_ptr)++] << 24;
	    pane_component->color += buf[(*offset_ptr)++] << 16;
	    pane_component->color += buf[(*offset_ptr)++] << 8;
	    pane_component->color += buf[(*offset_ptr)++];
	}

	return 0;
}

void print_pane_component_Line(FILE* fp, Pane_Component_Line* pcL){
	fprintf(fp,"%d\n",pcL->component_type);
	fprintf(fp,"%d\n",pcL->unknown);
	fprintf(fp,"%d\n",pcL->pos_left);
	fprintf(fp,"%d\n",pcL->pos_top);
	fprintf(fp,"%d\n",pcL->pos_right);
	fprintf(fp,"%d\n",pcL->pos_bottom);
	fprintf(fp,"%d\n",pcL->width);
	fprintf(fp,"%d\n",pcL->ntype);
	fprintf(fp,"%d\n",pcL->color);
}