#include "ts.h"

int adjust_TS_packet_header( TS_packet_header* TS_header,unsigned char* buf)
{
	TS_header->transport_error_indicator = buf[1] >> 7;
	TS_header->payload_unit_start_indicator = buf[1] >> 6 & 0x01;
	TS_header->transport_priority = buf[1] >> 5 & 0x01;
	TS_header->PID = (buf[1] & 0x1F) << 8 | buf[2];
	TS_header->transport_scrambling_control = buf[3] >> 6;
	TS_header->adaption_field_control = buf[3] >> 4 & 0x03;
	TS_header->continuity_counter = buf[3] & 0x0F; // 四位数据,应为0x0F xyy 09.03.18
	return 0;
}