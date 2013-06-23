#include "pes.h"

int adjust_PES_packet_header( PES_packet_header* PES_header,unsigned char* buf)
{
	//buf 5 byte
	PES_header->table_id = buf[0];
	PES_header->section_syntax_indicator = buf[1] >> 7;
	PES_header->reserved = (buf[1] >> 4) & 0x7;
	PES_header->section_length = (buf[1] & 0xF)*256 + buf[2];
	PES_header->table_extension_id = (buf[3] * 256) + buf[4];
	return 0;
}