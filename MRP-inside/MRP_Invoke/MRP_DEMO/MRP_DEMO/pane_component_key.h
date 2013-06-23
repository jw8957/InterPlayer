#ifndef __PANE_COMPONENT_KEY_H_
#define __PANE_COMPONENT_KEY_H_
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_Key
{
	unsigned component_type :8;
	unsigned unknown :8;
	unsigned kcode :8;
	//pos
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned href_flag :8;
	unsigned href_info :16;
	unsigned length_of_info :16;
	unsigned char *info;
	unsigned return_flag :8;
	unsigned length_of_return_info :16;
	unsigned char *return_info;

} Pane_Component_Key;

int free_pane_component_key(Pane_Component_Key* pane_component);
int adjust_Pane_component_Key(Pane_Component_Key* pane_component,unsigned char* buf, int* offset_ptr, int length);

void print_pane_component_Key(FILE* fp, Pane_Component_Key* pcK);
#endif