#ifndef __SYN_INF_STRUCT_H_
#define __SYN_INF_STRUCT_H_
#include "pane.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct SYN_INF_STRUCT
{
	unsigned ntime :32;
	unsigned duration :32;
	unsigned main_pane_id :16;
	unsigned sub_pane_count :8; //指定有多少个pane
	unsigned reserved1 :32;
	//生成大小合适的数组来容纳pane
	PANE * panes;
} SYN_INF_STRUCT;

int free_syn_inf_struct(SYN_INF_STRUCT* syn_inf_struct);

int adjust_SYN_INF_STRUCT(SYN_INF_STRUCT* syn_inf_struct,unsigned char* buf, int length);

void print_SYN_INF_STRUCT(FILE* fp,SYN_INF_STRUCT* syn_inf_struct);

#endif