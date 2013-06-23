#ifndef __SCENE_SECTION_H_
#define __SCENE_SECTION_H_
typedef struct Scene_section
{
    unsigned section_number : 8;
	unsigned last_section_number :8;
	unsigned data_length : 32;
	unsigned current_trans_data_length :32;
	unsigned current_trans_offset :32;
} Scene_section;
int adjust_Scene_section(Scene_section* scene_section,unsigned char* buf);
#endif