#ifndef __TS_H_
#define __TS_H_


//ts��ͷ
typedef struct TS_packet_header
{
	unsigned sync_byte : 8; //ͬ���ֽ�, �̶�Ϊ0x47,��ʾ�������һ��TS����
	unsigned transport_error_indicator : 1; //��������ָʾ��
	unsigned payload_unit_start_indicator : 1; //��Ч���ص�Ԫ��ʼָʾ��
	unsigned transport_priority : 1; //��������, 1��ʾ�����ȼ�,������ƿ����õ��������ò���
	unsigned PID : 13; //PID
	unsigned transport_scrambling_control : 2; //������ſ��� 
	unsigned adaption_field_control : 2; //����Ӧ���� 01������Ч���أ�10���������ֶΣ�11���е����ֶκ���Ч���ء�Ϊ00�����������д���
	unsigned continuity_counter : 4; //���������� һ��4bit�ļ���������Χ0-15
} TS_packet_header;

//����ts��ͷ
int adjust_TS_packet_header( TS_packet_header* TS_header,unsigned char* buf);






#endif