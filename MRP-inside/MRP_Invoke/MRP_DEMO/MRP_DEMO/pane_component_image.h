#ifndef __PANE_COMPONENT_IMAGE_H_
#define __PANE_COMPONENT_IMAGE_H_
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_Image
{
	//注意这里type已经被读过了,直接赋值就行
	unsigned component_type :8;
	//pos
	unsigned unknown :8;
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned mime_type :8;
	//为0
	unsigned href_flag :8;
	unsigned length_of_pic :32;
	//保存图片的数据
	unsigned char* pic;

} Pane_Component_Image;

int free_pane_component_image(Pane_Component_Image* pane_component);
int adjust_Pane_component_Image(Pane_Component_Image* pane_component,unsigned char* buf, int* offset_ptr,int length);
void print_pane_component_Image(FILE* fp, Pane_Component_Image* pcI);

#endif