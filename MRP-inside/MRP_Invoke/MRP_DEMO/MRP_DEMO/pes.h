#ifndef __PES_H_
#define __PES_H_
//pes��ͷ
typedef struct PES_packet_header
{
	unsigned table_id :8;
	unsigned section_syntax_indicator :1;
	unsigned reserved :3;
	unsigned section_length :12;
    unsigned table_extension_id :16;
} PES_packet_header;
//����pes��ͷ
int adjust_PES_packet_header( PES_packet_header* PES_header,unsigned char* buf);

#endif