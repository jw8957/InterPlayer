using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTry
{
    class TS_info
    {
        public TS_info() { }

        public TS_info(int _mstime,String _message) {
            this.ms_time = _mstime;
            this.message = _message;
        }

        public int getTime() {
            return ms_time;
        }

        public String getMessage() {
            return message;
        }

        private int ms_time;
        private String message;

        public static void getInfo(Queue<TS_info> q,String fname)   {
            if (fname.IndexOf("广") > 0){
                q.Enqueue(new TS_info(4000, "杜牧，字牧之，号称杜紫薇。晚唐诗人。晚唐时期.唐代文学家，后人称杜甫为'老杜'，称杜牧为'小杜'。《阿房宫赋》亦颇有名。\n杰出的诗人、散文家，是宰相杜佑之孙，杜从郁之子，唐文宗大和二年26岁中进士，授弘文馆校书郎。史馆修撰，膳部、比部、司勋员外郎，黄州、池州、睦州刺史等职，最终官至中书舍人。"));
                q.Enqueue(new TS_info(20000, "诗歌通过诗人的感情倾向，以枫林为主景，绘出了一幅色彩热烈、艳丽的山林秋色图。远上秋山的石头小路，首先给读者一个远视。山路的顶端是白云缭绕的地方。路是人走出来的，因此白云缭绕而不虚无飘缈，寒山蕴含着生气，“白云生处有人家”一句就自然成章。然而这只是在为后两句蓄势，接下来诗人明确地告诉读者，那么晚了，我还在山前停车，只是因为眼前这满山如火如荼，胜于春花的枫叶。与远处的白云和并不一定看得见的人家相比，枫林更充满了生命的纯美和活力。"));
            }
            else {
                q.Enqueue(new TS_info(4000, "鼎是青铜器的最重要青铜器物种之一，是用以烹煮肉和盛贮肉类的器具。三代及秦汉延续两千多年，鼎一直是最常见和最神秘的礼器。"));
                q.Enqueue(new TS_info(20000, "篆书是大篆、小篆的统称。大篆指甲骨文、金文、籀文、六国文字，它们保存着古代象形文字的明显特点。小篆也称“秦篆”，是秦国的通用文字，大篆的简化字体，其特点是形体匀逼齐整、字体较籀文容易书写。在汉文字发展史上，它是大篆由隶、楷之间的过渡。"));
            }
        }
    }

    enum pType { info, question };

    class pane {
        private int id;
        private int pos;
        private pType type;
        private String text;
        private String img;
        private char answer;

        public static void getInfo(Queue<pane> q, String fname) {
            if (fname.IndexOf("广") > 0) {
                pane p = new pane(pType.info);
                p.Img = (@"./scr/杜牧.jpg");
                p.Text = "  杜牧，字牧之，号称杜紫薇。晚唐诗人。晚唐时期.唐代文学家，后人称杜甫为'老杜'，称杜牧为'小杜'。《阿房宫赋》亦颇有名。\n  杰出的诗人、散文家。后赴江西观察使幕，转淮南节度使幕，又入观察使幕。史馆修撰，膳部、比部、司勋员外郎，黄州、池州、睦州刺史等职，最终官至中书舍人。";
                p.Pos=2500;
                q.Enqueue(p);

                p = new pane(pType.question);
                p.Text = "该诗的背景是哪个朝代？\n  A.唐朝\n  B.宋朝\n  C.明朝\n  D.清朝";
                p.Answer='a';
                p.Pos = 15000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/tssbs.jpg");
                p.Text = "唐诗三百首\n定　　价：￥88.00\n出 版 社：吉林出版集团有限责任公司\n出版时间：2009-11-1\n版　　次：1\n页　　数：189\n字　　数：70000\n印刷时间：2009-11-1\n开　　本：大16开\n纸　　张：轻型纸\n印　　次：1\nI S B N：9787546310527\n包　　装：精装";
                p.Pos = 35000;
                q.Enqueue(p);

                p = new pane(pType.info);
                //p.Img = (@"./scr/tssbs.jpg");
                p.Text = "唐诗三百首\n定　　价：￥88.00\n出 版 社：吉林出版集团有限责任公司\n出版时间：2009-11-1\n版　　次：1\n页　　数：189\n字　　数：70000\n印刷时间：2009-11-1\n开　　本：大16开\n纸　　张：轻型纸\n印　　次：1\nI S B N：9787546310527\n包　　装：精装";
                p.Pos = 48000;
                q.Enqueue(p);
            }
            else if (fname.IndexOf("书") > 0) {
                pane p = new pane(pType.info);
                p.Text = "鼎是青铜器的最重要青铜器物种之一，是用以烹煮肉和盛贮肉类的器具。三代及秦汉延续两千多年，鼎一直是最常见和最神秘的礼器。";
                p.pos = 10000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Text = "篆书是大篆、小篆的统称。大篆指甲骨文、金文、籀文、六国文字，它们保存着古代象形文字的明显特点。小篆也称“秦篆”，是秦国的通用文字，大篆的简化字体，其特点是形体匀逼齐整、字体较籀文容易书写。在汉文字发展史上，它是大篆由隶、楷之间的过渡。";
                p.pos = 50000;
                q.Enqueue(p);
                //q.Enqueue(new TS_info(20000, "篆书是大篆、小篆的统称。大篆指甲骨文、金文、籀文、六国文字，它们保存着古代象形文字的明显特点。小篆也称“秦篆”，是秦国的通用文字，大篆的简化字体，其特点是形体匀逼齐整、字体较籀文容易书写。在汉文字发展史上，它是大篆由隶、楷之间的过渡。"));
            }
            else if (fname.IndexOf("足球") > 0) {
                pane p = new pane(pType.info);
                p.Text = "天下足球\nCCTV5\n播放时间：每周五21:30";
                p.pos = 10000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Text = "国家德比\n皇家马德里VS巴塞罗那\n毫无疑问皇马和巴萨是西甲历史上最成功的两支球队，可以说他们统治了西班牙足坛。";
                p.pos = 40000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Text = "目前欧洲联赛情况：\n意甲：出结果\n西甲：明朗\n德甲：扑朔迷离";
                p.pos = 100000;
                q.Enqueue(p);

                p = new pane(pType.question);
                p.Text = "穆里尼奥执教过的皇马战胜过几次巴萨？\nA.1次\nB.两次\nC.三次\nD.四次";
                p.Answer='D';
                p.pos = 150000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Text = "皇马首发\n门将 卡西利亚斯（队长）\n后卫 马塞洛 佩佩 拉莫斯 阿贝罗亚\n后腰 阿隆索 赫迪拉\n前卫 C罗 卡卡/厄齐尔 迪马利亚\n中锋 本泽马/伊瓜因";
                p.pos = 200000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Text = "门将 巴尔德斯\n后卫 阿尔维斯 皮克 普约尔 阿比达尔\n后腰 布斯克茨\n中场 哈维 伊涅斯塔\n前锋 梅西 法布雷加斯 桑切斯";
                p.pos = 300000;
                q.Enqueue(p);

                p = new pane(pType.question);
                p.Text = "这个球进了吗？\nA.进   B.没进";
                p.Answer = 'A';
                p.pos = 1050000;
                q.Enqueue(p);
            }
        }

        public pane(pType _type) {
            type = _type;
            img = null;
        }

        public pane(int _id,int _pos, pType _type,String _text, String _img,char _answer) {
            id = _id;
            pos = _pos;
            type = _type;
            text = _text;
            img = _img;
            answer = _answer;
        }

        public int Id {
            get { return id;  }
            set { id = value; }
        }

        public int Pos {
            get { return pos;  }
            set { pos = value; }
        }

        public pType Type {
            get { return type; }
            set { type = value;}
        }

        public String Text {
            get { return text; }
            set { text = value; }
        }

        public String Img {
            get { return img; }
            set { img = value; }
        }

        public char Answer {
            get { return answer; }
            set { answer = value; }
        }
    }
}