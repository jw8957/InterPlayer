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
                p.pos = 15000;
                q.Enqueue(p);

                p = new pane(pType.question);
                //p.Img = (@"./scr/梅西.jpg");
                p.Text = "上赛季皇马巴萨的胜负情况如何？\nA.皇马全胜\nB.皇马胜率高\nC.巴萨胜率高\nD.巴萨全胜";
                p.answer = 'c';
                p.pos = 30000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/梅西.jpg");
                p.Text = "里奥内尔·安德雷斯·梅西（Lionel Andrés Messi），生于1987年6月24日，是一名阿根廷足球运动员。\n司职前锋同时可以兼任攻击型中场，现效力于巴塞罗那，他被大众称为“新马拉多纳“。";
                p.pos = 39000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/C罗.jpg");
                p.Text = "克里斯蒂亚诺·罗纳尔多(Cristiano Ronaldo，简称“C·罗纳尔多”或“C罗”），是一名葡萄牙足球运动员，司职边锋同时也可兼任中锋.\n现效力于西甲豪门皇家马德里队，同时身兼葡萄牙国家队的队长。他带球速度极快，善于突破和射门，拥有强悍的身体素质，技术都非常全面，是当今世界足坛最杰出的球星之一。\nC罗18岁加入英超曼联，6年来为曼联赢得众多主要赛事荣誉，也获得不少个人荣誉，其中在2007-08年赛季个人攻入42球，帮助曼联获得欧冠冠军和卫冕英超冠军，因此荣膺欧洲金球奖和世界足球先生，是首位在英超诞生的世界足球先生。";
                p.pos = 85000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/飞科.jpg");
                p.Text = "飞科剃须刀\n全方位浮动剃须\n三环弧面刀网\n全身水洗";
                p.pos = 105000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/多特蒙德.jpg");
                p.Text = "2011-2012德甲联赛冠军为多特蒙德";
                p.pos = 118000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/诺坎普球场.jpg");
                p.Text = "诺坎普球场，国内亦译为坎帕诺球场，是西甲豪门巴塞罗那队的主场。\n1957年完工时只能容纳93053名观众（原计划容纳150000人，后被取消），现能容纳109815名观众。球场面积为107x72米（现在诺坎普是 105x68，符合欧足联的要求）。主要材料为混凝土和钢铁。";
                p.pos = 226000;
                q.Enqueue(p);

                p = new pane(pType.question);
                p.Text = "这个角球会不会进？\nA.会\nB.不会\n";
                p.Answer='B';
                p.pos = 304000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/赫迪拉.jpg");
                p.Text = "萨米·赫迪拉（Sami Khedira，1987年4月4日－）出生在斯图加特，是一名德国足球运动员，2010年夏天以1400万欧元的身价从德甲的斯图加特足球俱乐部转会至西甲豪门皇家马德里。";
                p.pos = 461000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/哈维.jpg");
                p.Text = "哈维·埃尔南德兹·克雷乌斯（Xavier Hernandez Creus，1980年1月25日－），是一名西班牙足球运动员，现在效力于西班牙的巴塞罗那足球俱乐部，司职中场。\n哈维·埃尔南德兹·克雷乌斯作为主力球员参加了世界杯、欧洲杯等比赛并获得欧洲杯最佳球员等荣誉的西班牙足球运动员。2012年7月2日，助西班牙队4-0战胜意大利队，夺得第14届欧洲杯冠军。";
                p.pos = 882000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/多特蒙德.jpg");
                p.Text = "普鲁士多特蒙德1909球类比赛俱乐部合资股份有限公司（德语：Ballspiel-Verein Borussia 1909 e.V. Dortmund，BVB），位于德国多特蒙德的足球俱乐部，位于德国鲁尔区多特蒙德市。\n多特蒙德在1997年曾经夺得过一次欧洲冠军联赛，这也是德甲除拜仁慕尼黑以外的球队最近的一次获得冠军杯。多特蒙德同沙尔克04之间的比赛被称为“鲁尔区德比”，和拜仁慕尼黑之间的比赛则是“德国国家德比”。";
                p.pos = 1332000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/香川真司.jpg");
                p.Text = "香川真司（1989年3月17日－），出生于日本神户市，足球运动员，2006年出道于日本职业足球联赛的大阪樱花足球俱乐部，于2010年7月转会德甲球队多特蒙德，后获得2010-2011赛季德甲半程最佳球员。\n2012年-2013年赛季，香川真司正式披上红魔战袍，为曼联效力。";
                p.pos = 1529000;
                q.Enqueue(p);

                p = new pane(pType.info);
                p.Img = (@"./scr/法兰西大球场.jpg");
                p.Text = "法兰西大球场（Stade de France）位于法国巴黎市郊的圣丹尼，是一个多种用途的大型运动场地，可容纳8万名观众。法兰西大球场是为1998年世界杯足球赛而兴建，并曾作为1998年世界杯决赛举行场地。\n1998年7月12日，法国在世界杯决赛以3-0击败巴西，在主场取得首次世界杯冠军。2003年则曾举办世界田径锦标赛。";
                p.pos = 2420000;
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