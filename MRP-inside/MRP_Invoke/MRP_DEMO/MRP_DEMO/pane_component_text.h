#ifndef __PANE_COMPONENT_TEXT_H_
#define __PANE_COMPONENT_TEXT_H_
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Pane_Component_Text
{
	//ע������type�Ѿ���������,ֱ�Ӹ�ֵ����
	unsigned component_type :8;
	//�������ת���������һ��λ���ƶ�Ĭ��Ϊ0
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
	//��Ҫ���ɴ�С���ʵ��ַ��������洢�ַ���
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