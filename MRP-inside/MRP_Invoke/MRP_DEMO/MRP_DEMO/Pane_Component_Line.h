#ifndef __PANE_COMPONENT_LINE_H_
#define __PANE_COMPONENT_LINE_H_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_Line
{
	unsigned component_type :8;
	unsigned unknown :8;
	//pos
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned width :8;
	unsigned ntype :8;
	unsigned color :32;
} Pane_Component_Line;

int free_pane_component_line(Pane_Component_Line* pane_component);
int adjust_Pane_component_line(Pane_Component_Line* pane_component,unsigned char* buf, int* offset_ptr, int length);

void print_pane_component_Line(FILE* fp, Pane_Component_Line* pcL);
#endif