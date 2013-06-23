#ifndef __PANE_H_
#define __PANE_H_

#include "Pane_component.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct PANE
{
	//headers
	unsigned pane_id :16;
	unsigned visible :8;
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned border_width :8;
	unsigned border_color1 :8;
	unsigned border_color2 :32;
	unsigned border_style :8;
	unsigned border_color11 :8;
	unsigned border_color12 :32;
	unsigned clear :8;
	unsigned total :8;
	unsigned reserved2 :32;
	Pane_component *pane_components;
}PANE;

int free_panes(PANE* panes, int count);
int adjust_Pane(PANE* panes,int pane_index,unsigned char* buf,int* offset_ptr, int length);

void print_Pane(FILE* fp,PANE pane);

#endif