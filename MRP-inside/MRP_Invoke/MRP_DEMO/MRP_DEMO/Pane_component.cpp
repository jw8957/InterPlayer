
#include "Pane_component.h"
int free_pane_component(Pane_component* pane_component, int pane_component_index)
{
	switch (pane_component[pane_component_index].pane_type)
	{
	case 0:
		if(pane_component[pane_component_index].pane_component_text != NULL)
		{
			free_pane_component_text(pane_component[pane_component_index].pane_component_text);
			free(pane_component[pane_component_index].pane_component_text);
		}
		break;
	case 1:
		if(pane_component[pane_component_index].pane_component_image != NULL)
		{
			free_pane_component_image(pane_component[pane_component_index].pane_component_image);
			free(pane_component[pane_component_index].pane_component_image);
		}
		break;
	case 2:
		if(pane_component[pane_component_index].pane_component_key != NULL)
		{
			free_pane_component_key(pane_component[pane_component_index].pane_component_key);
			free(pane_component[pane_component_index].pane_component_key);
		}
		break;
	case 3:
		if(pane_component[pane_component_index].pane_component_video_box != NULL)
		{
			free_pane_component_video_box(pane_component[pane_component_index].pane_component_video_box);
			free(pane_component[pane_component_index].pane_component_video_box);
		}
		break;
	case 4:
		if(pane_component[pane_component_index].pane_component_line != NULL)
		{
			free_pane_component_line(pane_component[pane_component_index].pane_component_line);
			free(pane_component[pane_component_index].pane_component_line);
		}
		break;
	case 5:
		if(pane_component[pane_component_index].pane_component_circle != NULL)
		{
			free_pane_component_circle(pane_component[pane_component_index].pane_component_circle);
			free(pane_component[pane_component_index].pane_component_circle);
		}
		break;
	case 6:
		if(pane_component[pane_component_index].pane_component_rect != NULL)
		{
			free_pane_component_rect(pane_component[pane_component_index].pane_component_rect);
			free(pane_component[pane_component_index].pane_component_rect);
		}
		break;
	default:
		break;
	}
	return 0;
}

int adjust_Pane_component(Pane_component* pane_component, int pane_component_index,unsigned char* buf, int *offset_ptr, int length)
{
	if(length < (*offset_ptr) + 1)
		return -1;
	pane_component[pane_component_index].pane_type = buf[(*offset_ptr)++];
	//printf("real type : %d\n",pane_component[pane_component_index].pane_type);
	(*offset_ptr)++;

	pane_component[pane_component_index].pane_component_text = NULL;
	pane_component[pane_component_index].pane_component_image = NULL;
	pane_component[pane_component_index].pane_component_key = NULL;
	pane_component[pane_component_index].pane_component_video_box = NULL;
	pane_component[pane_component_index].pane_component_line = NULL;
	pane_component[pane_component_index].pane_component_circle = NULL;
	pane_component[pane_component_index].pane_component_rect = NULL;

	switch (pane_component[pane_component_index].pane_type)
	{
	case 0:
		//printf("Pane_Component_Text\n");
		
		pane_component[pane_component_index].pane_component_text = (Pane_Component_Text *)malloc(sizeof(Pane_Component_Text));
		adjust_Pane_component_text(pane_component[pane_component_index].pane_component_text,buf,offset_ptr,length);
		break;
	case 1:
		//printf("Pane_Component_Image\n");

		pane_component[pane_component_index].pane_component_image = (Pane_Component_Image *)malloc(sizeof(Pane_Component_Image));
		adjust_Pane_component_Image(pane_component[pane_component_index].pane_component_image,buf,offset_ptr,length);
		break;
	case 2:
		//printf("Pane_Component_Key\n");

		pane_component[pane_component_index].pane_component_key = (Pane_Component_Key *)malloc(sizeof(Pane_Component_Key));
		adjust_Pane_component_Key(pane_component[pane_component_index].pane_component_key,buf,offset_ptr,length);
		break;
	case 3:
		//printf("Pane_Component_VideoBox\n");

		pane_component[pane_component_index].pane_component_video_box = (Pane_Component_VideoBox *)malloc(sizeof(Pane_Component_VideoBox));
		adjust_Pane_component_video_box(pane_component[pane_component_index].pane_component_video_box,buf,offset_ptr,length);
		break;
	case 4:
		//printf("Pane_Component_Line\n");

		pane_component[pane_component_index].pane_component_line = (Pane_Component_Line *)malloc(sizeof(Pane_Component_Line));
		adjust_Pane_component_line(pane_component[pane_component_index].pane_component_line,buf,offset_ptr,length);
		break;
	case 5:
		//printf("Pane_Component_Circle\n");

		pane_component[pane_component_index].pane_component_circle = (Pane_Component_Circle *)malloc(sizeof(Pane_Component_Circle));
		adjust_Pane_component_circle(pane_component[pane_component_index].pane_component_circle,buf,offset_ptr,length);
		break;
	case 6:
		//printf("Pane_Component_Rect\n");

		pane_component[pane_component_index].pane_component_rect = (Pane_Component_Rect *)malloc(sizeof(Pane_Component_Rect));
		adjust_Pane_component_rect(pane_component[pane_component_index].pane_component_rect,buf,offset_ptr,length);
		break;
	default:
		//printf("no such type of pane components! %d\n",pane_component[pane_component_index].pane_type);
		break;
	}

	return 0;
}

/*
 *0 Pane_Component_Text* pane_component_text; 
 *1 Pane_Component_Image* pane_component_image; 
 *2 Pane_Component_Key* pane_component_key; 
 *3 Pane_Component_VideoBox* pane_component_video_box; 
 *4 Pane_Component_Line* pane_component_line;
 *5 Pane_Component_Circle* pane_component_circle; 
 *6 Pane_Component_Rect* pane_component_rect; 
**/


void print_Pane_component(FILE* fp,Pane_component pc ){
	fprintf(fp,"%d\n",pc.pane_type);
	switch (pc.pane_type)
	{
	case 0:
		//printf("pane_component_text\n");
		print_pane_component_text(fp,pc.pane_component_text);
		break;
	case 1:
		//printf("pane_component_image\n");
		//print_pane_component_Image(fp,pc.pane_component_image);
		break;
	case 2:
		//printf("pane_component_Key\n");
		print_pane_component_Key(fp,pc.pane_component_key);
		break;
	case 3:
		//printf("pane_component_VideoBox\n");
		//print_pane_component_VBox(fp,pc.pane_component_video_box);
		break;
	case 4:
		//printf("pane_component_Line\n");
		//print_pane_component_Line(fp,pc.pane_component_line);
		break;
	case 5:
		//printf("pane_component_Circle\n");
		//print_pane_component_Circle(fp,pc.pane_component_circle);
		break;
	case 6:
		//printf("pane_component_Rect\n");
		//print_pane_component_rect(fp,pc.pane_component_rect);
		break;
	default:
		break;
	}
}