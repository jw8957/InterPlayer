#ifndef __PANE_COMPONENT_H_
#define __PANE_COMPONENT_H_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "pane_component_text.h"
#include "pane_component_image.h"
#include "pane_component_key.h"
#include "Pane_Component_VideoBox.h"
#include "Pane_Component_Line.h"
#include "Pane_Component_Rect.h"
#include "Pane_Component_Circle.h"

typedef struct Pane_component
{
	int pane_type;
	Pane_Component_Text* pane_component_text; 
	Pane_Component_Image* pane_component_image; 
	Pane_Component_Key* pane_component_key; 
	Pane_Component_VideoBox* pane_component_video_box; 
	Pane_Component_Line* pane_component_line;
	Pane_Component_Circle* pane_component_circle; 
	Pane_Component_Rect* pane_component_rect; 

} Pane_component;

int free_pane_component(Pane_component* pane_component, int pane_component_index);
int adjust_Pane_component(Pane_component* pane_component, int pane_component_index,unsigned char* buf, int *offset_ptr, int length);
void print_Pane_component(FILE* fp,Pane_component pc);

#endif