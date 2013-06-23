
#include "pane.h"
int free_panes(PANE* panes, int count)
{
	for(int i = 0; i < panes[count].total; i++)
	{
		free_pane_component(panes[count].pane_components, i);
	}
	free(panes[count].pane_components);
	return 0;
}

int adjust_Pane(PANE* panes,int pane_index,unsigned char* buf,int* offset_ptr, int length)
{
	panes[pane_index].pane_id = 0;
	panes[pane_index].visible = 0;
	panes[pane_index].pos_left = 0;
	panes[pane_index].pos_top = 0;
	panes[pane_index].pos_right = 0;
	panes[pane_index].pos_bottom = 0;
	panes[pane_index].border_width = 0;
	panes[pane_index].border_color1 = 0;
	panes[pane_index].border_color2 = 0;
	panes[pane_index].border_style = 0;
	panes[pane_index].border_color11 = 0;
	panes[pane_index].border_color12 = 0;
	panes[pane_index].clear = 0;
	panes[pane_index].total = 0;
	panes[pane_index].reserved2 = 0;
	panes[pane_index].pane_components = NULL;


	if(length < (*offset_ptr) + 13)
		return -1;
	panes[pane_index].pane_id = buf[(*offset_ptr)++] << 8;
	panes[pane_index].pane_id += buf[(*offset_ptr)++];

	//printf("pane_id %d\n",panes[pane_index].pane_id);
	panes[pane_index].visible = buf[(*offset_ptr)++];

	panes[pane_index].pos_left = buf[(*offset_ptr)++] << 8;
	panes[pane_index].pos_left += buf[(*offset_ptr)++];
	//printf("pane_left %d\n",panes[pane_index].pos_left);

	panes[pane_index].pos_top = buf[(*offset_ptr)++] << 8;
	panes[pane_index].pos_top += buf[(*offset_ptr)++];
	//printf("pane_top %d\n",panes[pane_index].pos_top);
	panes[pane_index].pos_right = buf[(*offset_ptr)++] << 8;
	panes[pane_index].pos_right += buf[(*offset_ptr)++];
	//printf("pane_right %d\n",panes[pane_index].pos_right);
	panes[pane_index].pos_bottom = buf[(*offset_ptr)++] << 8;
	panes[pane_index].pos_bottom += buf[(*offset_ptr)++];
	//printf("pane_bottom %d\n",panes[pane_index].pos_bottom);
	panes[pane_index].border_width = buf[(*offset_ptr)++];
	panes[pane_index].border_color1 = buf[(*offset_ptr)++];
	if(panes[pane_index].border_color1 == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		panes[pane_index].border_color2 = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
		panes[pane_index].border_color2 = buf[(*offset_ptr)++] << 24;
	    panes[pane_index].border_color2 += buf[(*offset_ptr)++] << 16;
	    panes[pane_index].border_color2 += buf[(*offset_ptr)++] << 8;
	    panes[pane_index].border_color2 += buf[(*offset_ptr)++];
	}
	if(length < (*offset_ptr) + 2)
		return -1;
	panes[pane_index].border_style = buf[(*offset_ptr)++];
	panes[pane_index].border_color11 = buf[(*offset_ptr)++];
	if(panes[pane_index].border_color11 == 0)
	{
		if(length < (*offset_ptr) + 1)
			return -1;
		panes[pane_index].border_color12 = buf[(*offset_ptr)++];
	}
	else
	{
		if(length < (*offset_ptr) + 4)
			return -1;
		panes[pane_index].border_color12 = buf[(*offset_ptr)++] << 24;
	    panes[pane_index].border_color12 += buf[(*offset_ptr)++] << 16;
	    panes[pane_index].border_color12 += buf[(*offset_ptr)++] << 8;
	    panes[pane_index].border_color12 += buf[(*offset_ptr)++];
	}
	if(length < (*offset_ptr) + 6)
		return -1;
	panes[pane_index].clear = buf[(*offset_ptr)++];
	panes[pane_index].total = buf[(*offset_ptr)++];
	panes[pane_index].reserved2 = buf[(*offset_ptr)++] << 24;
	panes[pane_index].reserved2 += buf[(*offset_ptr)++] << 16;
	panes[pane_index].reserved2 += buf[(*offset_ptr)++] << 8;
	panes[pane_index].reserved2 += buf[(*offset_ptr)++];

	//创建合适大小的数组空间来容纳pane组件
	panes[pane_index].pane_components = (Pane_component *)malloc(sizeof(Pane_component) * panes[pane_index].total);
	//printf("\npanes_total_com:%d\n",panes[pane_index].total);
	//读取子组件的属性
	for(int i = 0; i < panes[pane_index].total; i++)
		adjust_Pane_component(panes[pane_index].pane_components,i,buf,offset_ptr,length);

	return 0;
}


void print_Pane(FILE* fp,PANE pane){
	fprintf(fp,"%d\n",pane.pane_id);
	fprintf(fp,"%d\n",pane.visible);
	fprintf(fp,"%d\n",pane.pos_left);
	fprintf(fp,"%d\n",pane.pos_top);
	fprintf(fp,"%d\n",pane.pos_right);
	fprintf(fp,"%d\n",pane.pos_bottom);
	fprintf(fp,"%d\n",pane.border_width);
	fprintf(fp,"%d\n",pane.border_color1);
	fprintf(fp,"%d\n",pane.border_color2);
	fprintf(fp,"%d\n",pane.border_style);
	fprintf(fp,"%d\n",pane.border_color11);
	fprintf(fp,"%d\n",pane.border_color12);
	fprintf(fp,"%d\n",pane.clear);
	fprintf(fp,"%d\n",pane.total);
	fprintf(fp,"%d\n",pane.reserved2);

	for(int i=0;i<pane.total;++i){
		print_Pane_component( fp,pane.pane_components[i] );
	}
}