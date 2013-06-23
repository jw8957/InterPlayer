#ifndef __SCENE_SHOW_SECTION_H_
#define __SCENE_SHOW_SECTION_H_

typedef struct Scene_show_section
{
    unsigned scene_id :16;
} Scene_show_section;

int adjust_Scene_show_section(Scene_show_section* scene_show_section,unsigned char* buf);
#endif