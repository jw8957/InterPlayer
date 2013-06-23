#ifndef __PANE_COMPONENT_RECT_H_
#define __PANE_COMPONENT_RECT_H_
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
typedef struct Pane_Component_Rect
{
	unsigned component_type :8;
	unsigned unknown :8;

	//pos
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned nwidth :8;
	unsigned ntype :8;
	unsigned ncolor :32;
	unsigned nflag :8;
	unsigned bg_type :8;
	unsigned bg_color :32;

} Pane_Component_Rect;

int free_pane_component_rect(Pane_Component_Rect* pane_component);
int adjust_Pane_component_rect(Pane_Component_Rect* pane_component,unsigned char* buf, int* offset_ptr,int length);

void print_pane_component_rect(FILE* fp, Pane_Component_Rect* pcR);
#endif