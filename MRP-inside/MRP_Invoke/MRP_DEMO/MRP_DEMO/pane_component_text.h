#ifndef __PANE_COMPONENT_TEXT_H_
#define __PANE_COMPONENT_TEXT_H_
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_Text
{
	//注意这里type已经被读过了,直接赋值就行
	unsigned component_type :8;
	//这里针对转码代码中有一个位的移动默认为0
	unsigned unknown :8;
	//pos
	unsigned pos_left :16;
	unsigned pos_top :16;
	unsigned pos_right :16;
	unsigned pos_bottom :16;
	unsigned size :8;
	unsigned style :8;
	unsigned len :16;
	unsigned color1 :8;
	unsigned color2 :32;
	unsigned bg_color1 :8;
	unsigned bg_color2 :32;
	unsigned alpha :8;
	unsigned length_of_string :16;
	//需要生成大小合适的字符数组来存储字符串
	unsigned char* text;
	unsigned href_flag :8;
	unsigned more_info :16;
	unsigned length_of_info :16;
	unsigned char *info;
} Pane_Component_Text;
int free_pane_component_text(Pane_Component_Text* pane_component);
int adjust_Pane_component_text(Pane_Component_Text* pane_component,unsigned char* buf, int* offset_ptr, int length);
void print_pane_component_text( FILE* fp, Pane_Component_Text* pct );

#endif