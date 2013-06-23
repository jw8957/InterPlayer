#include "scene_show_section.h"

int adjust_Scene_show_section(Scene_show_section* scene_show_section,unsigned char* buf)
{
	//buf 2 byte
	scene_show_section->scene_id = buf[0] * 256 + buf[1];
	return 0;
}