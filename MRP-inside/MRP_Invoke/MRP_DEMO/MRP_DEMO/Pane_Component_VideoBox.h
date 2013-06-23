#ifndef __PANE_COMPONENT_VIDEOBOX_H_
#define __PANE_COMPONENT_VIDEOBOX_H_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_VideoBox
{
	unsigned component_type :8;
	unsigned unknown :8;
	//pos
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	//编码代码中还未实现后续功能
} Pane_Component_VideoBox;

int free_pane_component_video_box(Pane_Component_VideoBox* pane_component);
int adjust_Pane_component_video_box(Pane_Component_VideoBox* pane_component,unsigned char* buf, int* offset_ptr, int length);

void print_pane_component_VBox(FILE* fp, Pane_Component_VideoBox* pcV);


#endif