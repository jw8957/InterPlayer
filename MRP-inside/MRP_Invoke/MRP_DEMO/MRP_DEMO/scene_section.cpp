#include "scene_section.h"
int adjust_Scene_section(Scene_section* scene_section,unsigned char* buf)
{
	//buf 14 byte
	scene_section->section_number = buf[0];
	scene_section->last_section_number = buf[1];
	scene_section->data_length = (buf[2] << 24) + (buf[3] << 16) + (buf[4] << 8) + buf[5];
	scene_section->current_trans_data_length = (buf[6] << 24) + (buf[7] << 16) + (buf[8] << 8) + buf[9];
	scene_section->current_trans_offset = (buf[10] << 24) + (buf[11] << 16) + (buf[12] << 8) + buf[13];

	return 0;
}