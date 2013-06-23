
#include "Pane_Component_VideoBox.h"


int free_pane_component_video_box(Pane_Component_VideoBox* pane_component)
{
	return 0;
}

int adjust_Pane_component_video_box(Pane_Component_VideoBox* pane_component,unsigned char* buf, int* offset_ptr, int length)
{

	pane_component->component_type = 0;
	pane_component->unknown = 0;
	pane_component->pos_left = 0;
	pane_component->pos_top = 0;
	pane_component->pos_right = 0;
	pane_component->pos_bottom = 0;

	pane_component->component_type = 3;
	if(length < (*offset_ptr) + 8)
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
    return 0;
}

void print_pane_component_VBox(FILE* fp, Pane_Component_VideoBox* pcV){
	fprintf(fp,"%d\n",pcV->component_type);
	fprintf(fp,"%d\n",pcV->unknown);
	fprintf(fp,"%d\n",pcV->pos_left);
	fprintf(fp,"%d\n",pcV->pos_top);
	fprintf(fp,"%d\n",pcV->pos_right);
	fprintf(fp,"%d\n",pcV->pos_bottom);
}