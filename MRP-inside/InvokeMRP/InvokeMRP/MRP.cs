using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvokeMRP
{
    class MRP
    {
        public MRP() { 
            syn_inf_struct_list=new Queue<SYN_INF_STRUCT>();
            slistSize = 0;
        }

        public void ReadFile(String filename) {
            fp = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
            SYN_INF_STRUCT syn_inf;
            slistSize=Int32.Parse( fp.ReadLine() );

            for (int i = 0; i < slistSize; ++i) {
                syn_inf = new SYN_INF_STRUCT();
                Read_SYN_INF_STRUCT(syn_inf);
                syn_inf_struct_list.Enqueue(syn_inf);
            }
        }

        void Read_SYN_INF_STRUCT(SYN_INF_STRUCT syn_inf) {
            syn_inf.ntime = Int32.Parse( fp.ReadLine() );
            syn_inf.duration = Int32.Parse(fp.ReadLine());
            syn_inf.main_pane_id = Int32.Parse(fp.ReadLine());
            syn_inf.sub_pane_count= Int32.Parse(fp.ReadLine());
            syn_inf.reserved1= Int32.Parse(fp.ReadLine());

            syn_inf.panes=new PANE[syn_inf.sub_pane_count];

            for (int i = 0; i < syn_inf.sub_pane_count; ++i)
            {
                syn_inf.panes[i] = new PANE();
                Read_Pane(syn_inf.panes[i]);
            }
        }

        void Read_Pane(PANE pane) {
            pane.pane_id = Int32.Parse(fp.ReadLine());
            pane.visible = Int32.Parse(fp.ReadLine());
            pane.pos_left = Int32.Parse(fp.ReadLine());
            pane.pos_top= Int32.Parse(fp.ReadLine());
            pane.pos_right = Int32.Parse(fp.ReadLine());
            pane.pos_bottom= Int32.Parse(fp.ReadLine());
            pane.border_width = Int32.Parse(fp.ReadLine());
            pane.border_color1 = Int32.Parse(fp.ReadLine());
            pane.border_color2 = Int32.Parse(fp.ReadLine());
            pane.border_style = Int32.Parse(fp.ReadLine());
            pane.border_color11 = Int32.Parse(fp.ReadLine());
            pane.border_color12 = Int32.Parse(fp.ReadLine());
            pane.clear = Int32.Parse(fp.ReadLine());
            pane.total = Int32.Parse(fp.ReadLine());
            pane.reserved2 = Int32.Parse( fp.ReadLine() );

            pane.pane_components=new Pane_component[pane.total];
            for (int i = 0; i < pane.total; ++i)
            {
                pane.pane_components[i] = new Pane_component();
                Read_pane_components(pane.pane_components[i]);
            }
        }

        void Read_pane_components(Pane_component pc) {
            pc.pane_type = Int32.Parse( fp.ReadLine() );
            switch (pc.pane_type)
            {
                case 0:
                    pc.pane_component_text = new Pane_Component_Text();
                    Read_Pane_component_text(pc.pane_component_text);
                    break;
                case 1: break;
                case 2: 
                    pc.pane_component_key = new Pane_Component_Key();
                    Read_Pane_component_key(pc.pane_component_key);
                    break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: break;
                default: break;
            }
        }

        void Read_Pane_component_text(Pane_Component_Text pct){
            pct.component_type = Int32.Parse(fp.ReadLine());
            pct.unknown = Int32.Parse(fp.ReadLine());
            pct.pos_left = Int32.Parse(fp.ReadLine());
            pct.pos_top = Int32.Parse(fp.ReadLine());
            pct.pos_right = Int32.Parse(fp.ReadLine());
            pct.pos_bottom = Int32.Parse(fp.ReadLine());
            pct.size = Int32.Parse(fp.ReadLine());
            pct.style = Int32.Parse(fp.ReadLine());
            pct.len = Int32.Parse(fp.ReadLine());
            //String tmp = fp.ReadLine();
            pct.color1 = Int32.Parse(fp.ReadLine());
            pct.color2 = Int32.Parse(fp.ReadLine());
            pct.bg_color1 = Int32.Parse(fp.ReadLine());
            //tmp = fp.ReadLine();
            pct.bg_color2 = Int32.Parse(fp.ReadLine());
            pct.alpha = Int32.Parse(fp.ReadLine());
            pct.length_of_string = Int32.Parse(fp.ReadLine());
            pct.text = fp.ReadLine();                   //乱码
            pct.href_flag = Int32.Parse(fp.ReadLine());
            pct.more_info = Int32.Parse(fp.ReadLine());
            pct.length_of_info = Int32.Parse(fp.ReadLine());
            pct.info = fp.ReadLine();
        }

        void Read_Pane_component_key(Pane_Component_Key pcK){
            pcK.component_type = Int32.Parse(fp.ReadLine());
            pcK.unknown = Int32.Parse(fp.ReadLine());
            pcK.kcode = Int32.Parse(fp.ReadLine());
            pcK.pos_left = Int32.Parse(fp.ReadLine());
            pcK.pos_top = Int32.Parse(fp.ReadLine());
            pcK.pos_right = Int32.Parse(fp.ReadLine());
            pcK.pos_bottom = Int32.Parse(fp.ReadLine());
            pcK.href_flag = Int32.Parse(fp.ReadLine());
            pcK.href_info = Int32.Parse(fp.ReadLine());
            pcK.length_of_info = Int32.Parse(fp.ReadLine());
            pcK.info = fp.ReadLine();
            pcK.return_flag = Int32.Parse(fp.ReadLine());
            pcK.length_of_return_info = Int32.Parse(fp.ReadLine());
            pcK.return_info = fp.ReadLine();
        }

        StreamReader fp;
        public Queue<SYN_INF_STRUCT> syn_inf_struct_list;
        int slistSize;
    }

    class SYN_INF_STRUCT{
        public int ntime;
        public int duration;
        public int main_pane_id;
        public int sub_pane_count; //指定有多少个pane
        public int reserved1;
	    //生成大小合适的数组来容纳pane
        public PANE[] panes;
    }

    class PANE{
        public int pane_id;
        public int visible;
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int border_width;
        public int border_color1;
        public int border_color2;
        public int border_style;
        public int border_color11;
        public int border_color12;
        public int clear;
        public int total;
        public int reserved2;
        public Pane_component[] pane_components;
    }

    class Pane_component{
        public int pane_type;
        public Pane_Component_Text pane_component_text;
        public Pane_Component_Image pane_component_image;
        public Pane_Component_Key pane_component_key;
        public Pane_Component_VideoBox pane_component_video_box;
        public Pane_Component_Line pane_component_line;
        public Pane_Component_Circle pane_component_circle;
        public Pane_Component_Rect pane_component_rect; 
    }
    
    class Pane_Component_Text
    {
	    //注意这里type已经被读过了,直接赋值就行
        public int component_type;
	    //这里针对转码代码中有一个位的移动默认为0
        public int unknown;
	    //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int size;
        public int style;
        public int len;
        public int color1;
        public int color2;
        public int bg_color1;
        public int bg_color2;
        public int alpha;
	    public int length_of_string;
	    //需要生成大小合适的字符数组来存储字符串
        public String text;
        public int href_flag;
        public int more_info;
        public int length_of_info;
        public String info;
    }

    class Pane_Component_Image
    {
        //注意这里type已经被读过了,直接赋值就行
        public int component_type;
        //pos
        public int unknown;
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int mime_type;
        //为0
        public int href_flag;
        public int length_of_pic;
        //保存图片的数据
        public string pic;
    }

    class Pane_Component_Key
    {
        public int component_type;
        public int unknown;
        public int kcode;
        //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int href_flag;
        public int href_info;
        public int length_of_info;
        public string info;
        public int return_flag;
        public int length_of_return_info;
        public string return_info;
    }

    class Pane_Component_VideoBox
    {
        public int component_type;
        public int unknown;
        //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        //编码代码中还未实现后续功能
    }

    class Pane_Component_Line
    {
        public int component_type;
        public int unknown;
        //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int width;
        public int ntype;
        public int color;
    }

    class Pane_Component_Circle
    {
        public int component_type;
        public int unknown;
        //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int nwidth;
        public int ntype;
        public int ncolor;
        public int nflag;
        public int bg_type;
        public int bg_color;
    }

    class Pane_Component_Rect
    {
        public int component_type;
        public int unknown;
	    //pos
        public int pos_left;
        public int pos_top;
        public int pos_right;
        public int pos_bottom;
        public int nwidth;
        public int ntype;
        public int ncolor;
        public int nflag;
        public int bg_type;
        public int bg_color;
    }
}