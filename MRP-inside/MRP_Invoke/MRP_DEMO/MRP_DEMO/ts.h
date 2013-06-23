#ifndef __TS_H_
#define __TS_H_


//ts包头
typedef struct TS_packet_header
{
	unsigned sync_byte : 8; //同步字节, 固定为0x47,表示后面的是一个TS分组
	unsigned transport_error_indicator : 1; //传输误码指示符
	unsigned payload_unit_start_indicator : 1; //有效荷载单元起始指示符
	unsigned transport_priority : 1; //传输优先, 1表示高优先级,传输机制可能用到，解码用不着
	unsigned PID : 13; //PID
	unsigned transport_scrambling_control : 2; //传输加扰控制 
	unsigned adaption_field_control : 2; //自适应控制 01仅含有效负载，10仅含调整字段，11含有调整字段和有效负载。为00解码器不进行处理
	unsigned continuity_counter : 4; //连续计数器 一个4bit的计数器，范围0-15
} TS_packet_header;

//解析ts包头
int adjust_TS_packet_header( TS_packet_header* TS_header,unsigned char* buf);






#endif