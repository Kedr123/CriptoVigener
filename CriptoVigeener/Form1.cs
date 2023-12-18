using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptoVigeener
{

    

    public partial class Form1 : Form
    {
        public string deshifr(char[] shifr, string key, string[] alpha)
        {
            string result = "";
            string resultK = "";
            int k = 0;
            for (int i = 0;i<shifr.Length; i++)
            {
                int indexS = alpha.ToList().IndexOf(shifr[i].ToString());
                int indexK = alpha.ToList().IndexOf(key[k].ToString());

                Debug.WriteLine(indexS);
                Debug.WriteLine(indexK);
                
                resultK += alpha[indexK];

                int resultIndex = (indexS - indexK);

                if (resultIndex < 0) resultIndex = alpha.Length + resultIndex;

                result += alpha[resultIndex% alpha.Length];

                if (k + 1 == key.Length) k = 0;
                else k++;
            }
            
            Debug.WriteLine(resultK);
            Debug.WriteLine(result);
            return result;
        }

        public List<MIcModel> MIc(List<List<string>> list, int M, string[] alpha)
        {
            var result = new List<MIcModel>();

            int i = 0;
            int j = 1;

            var re1 = new List<int>();

            for (;i<list.Count;)
            {
                if (i + 1 == list.Count && j + 1 >= list.Count) break;

                re1.Clear();
                Debug.WriteLine(j.ToString());
                for (int m = 0; m < M; m++)
                {
                    re1.Add(SumPiPj(list[i], list[j],m, alpha));
                }

                var micM = new MIcModel();
                micM.i= i; micM.j = j;
                micM.numders = re1;
                micM.numderMax = re1.Max();
                micM.numderMaxIndex = re1.IndexOf(micM.numderMax);

                result.Add(micM);

                if (i+1 == list.Count && j+1==list.Count) break;
                if (j + 1 == list.Count)
                {
                    i++;
                    j = i + 1;
                }
                else j++;

            }

            return result;
        }

        public int SumPiPj(List<string> listPi, List<string> listPj, int B,string[] alpha)
        {
            var alphaB = alpha.ToList();

            for(int i = 0; i < B; i++)
            {
                alphaB.Add(alpha[i]);
            }

            int result = 0;
            for(int i = 0; i<alpha.Length; i++)
            {
                result += Pi(listPi, alpha[i]) * Pi(listPj, alphaB[i+B]);
            }

            return result;
        }

        public int Pi(List<string> list, string item)
        {
            int x = 0;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i] == item)
                {
                    x++;
                }
            }
            return x;
        }

        public List<int> getCoifictent(List<MIcModel> list, int lenKey, int M)
        {
            var result = new List<int>();
            var isItems = new List<int>();
            var MaxItems = new List<MIcModel>();
            
            // Заполняем массив isItem значениями от 0 до длины ключа
            for(int i = 0; i < lenKey;i++)
            {
                isItems.Add(i);
            }

            // Данный цикл 
            foreach(var item in list)
            {
                if (isItems.Count == 0) break;

                // Добавляем элемент массива в MaxItems
                MaxItems.Add(item);

                // Перебераем все элементы массива isItems
                for (int f =0; f<isItems.Count; f++)
                {
                    // Если число под индексом isItems[f] равно i или j элемента входного массива, то значение с индексом f удаляется из массива isItems
                    if (isItems[f] == item.i || isItems[f] == item.j)
                    {
                        //MaxItems.Add(item);
                        isItems.RemoveAt(f);
                        //break;
                    }
                }
            }

            dataGridView3.Rows.Clear();

            for (int i = 0;i < MaxItems.Count; i++)
            {
                List<string> items = new List<string>();


                dataGridView3.Rows.Add(MaxItems[i].i, MaxItems[i].j, string.Join(",", MaxItems[i].numders));
            }
            Debug.WriteLine(MaxItems);

            // Предпологаем, что индекс первого символа ключа это 0
            result.Add(0);
            
            // Данный цикал с помощью рекурсивного метода находит сдвиги остальных букв отнасительно нуля
            for(int i = 1; i<lenKey; i++)
            {
                var j = recursiaGetX(MaxItems, i, M);
                if (j == null)
                {
                    result.Add(0);
                }
                result.Add(M-(Convert.ToInt32(j)%M));
            }
            Debug.WriteLine(result);
            // Возврат результата
            return result;
        }

        public int? recursiaGetX(List<MIcModel> MaxItems, int k, int M)
        {
            //Debug.WriteLine("k"+k.ToString()+" || i: "+I.ToString()+" || j: "+J.ToString());
            List<MIcModel> MaxItemsCopy = MaxItems.ToList();

            for (int i = 0; i<MaxItems.Count; i++)
            {

                if (MaxItems[i].i == k)
                {
                    if (MaxItems[i].j == 0) return MaxItems[i].numderMaxIndex;
                    else
                    {
                        MaxItemsCopy.RemoveAt(i);
                        var x = recursiaGetX(MaxItemsCopy, MaxItems[i].j, M);

                        if(x != null) return x + MaxItems[i].numderMaxIndex;
                    }
                }
                else if (MaxItems[i].j == k)
                {
                    if (MaxItems[i].i == 0) return M - MaxItems[i].numderMaxIndex;
                    else
                    {
                        MaxItemsCopy.RemoveAt(i);
                        var x = recursiaGetX(MaxItemsCopy, MaxItems[i].i, M);

                        if (x != null) return x +(M - MaxItems[i].numderMaxIndex);
                    }
                }
            }

            return null;
        }

        public List<string> getKeys(List<int> zdvig, string[] alpha )
        {
            List<string> keys = new List<string>();

            string bufer = "";

            for (int i = 0; i < alpha.Length; i++)
            {
                bufer = "";

                for (int s = 0; s < zdvig.Count; s++)
                {
                    int index = (i + zdvig[s]);
                    if (index!=0) bufer += alpha[index%alpha.Length];
                    else bufer += alpha[0];
                }

                keys.Add(bufer);
            }

            return keys;

        }

        public Form1()
        {
            InitializeComponent();
            this.Width = 1500;
            this.Height = 1000;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Массив Алфавита
            string[] alpha = {"а", "б", "в", "г", "д", "е",
                              "ё", "ж", "з", "и", "й", "к",
                              "л", "м", "н", "о", "п", "р",
                              "с", "т", "у", "ф", "х", "ц",
                              "ч", "ш", "щ", "ъ", "ы", "ь",
                              "э", "ю", "я" };
            // Длина алфавита
            int M = alpha.Length;
            // Получение зашифрованного текста и удаление пробелов
            string text = textBox1.Text.ToLower();
            // string text = "ваыжрцец аояжхозапск эчьвнгуэм юруащфмлелщ пэсусганчдрб фябыц шнгеьрцщйм нкюьфрщмрщч ыястеэчш бьаоъфьчвгнзв яебсырпюче пвшлосм вэрзъ ълиь дмбтмю ъмюрш нрряхэы ылйгйнйз ясынфнюоэюн эбудкяааы арърпюо саиюмэы эацэншоомны ьюдбомщу ьоеюыщ ырррзъш цяззвтоанщря юруьчв арсрюсганчдрб ыщрычше тоушутьсвтх рыя еоьш жъцжхйюясип пьфтфыгп вы оэиэащфй ыэнрзмяфлщ уэыйдьсвтх оксякър плёзвтоы яошивфущьюё иююыефонлтфш тберыбтрвчкйю жлбочхф вязшълщэфги ршо сщсюрсж ыгвсыоюгя углцючв юаес уеьо щр члщ сфныфэазнъ цец ысчея ьюкрзлюбэн ндрю ъп сяцфлрзьс ярхсэтщрънещьюъ нмгшоюачзтжш тбосчв иурлрч нойюук эюлм в аъхшчуявмъши юонжъ ыязфлыуфнщй ыьурщхр оьхбаюы шлпэчпрлиъю пядьъёщэ урвъзь осрлууш ацйеюясуощлк чрэущя ьэфдясюлжчнзг шхэюкще нъмшэйюоюяш дья аъхш рсшдсцбттик ыхфьлэал ою вюишлтфу тякмфптхлф яцыуыюоюяш влсъцур щгзеюясо аоуфыфэрюыв хбсьепъжльлъ пэсуовтлнркух ииэыъих въусъхрясях улп сфэчрыю эаююювягъ яьлахщя ъмзе фечъ тр бгы оръюзюагщу цон эоусв пяклуеюкфп каэб нр съвнлъяюо ыэшеютфьунорюыц ъпцщощлрзьюъ пэыфкг иоьерб ержъан рялз н щъяпщрыопнщи щъжжд тбершюжхнфхчлщйхкмч бляжфнэлнфп сяэгкгуьл уьсгюифмёищ пьриэбгтллсв сябъх нщбзбеюъкй нкэыйьчпхня ьаотеьцн мъгуоьэшягнжб фряфаечяшв пвчкдэк евепы ыииь глцюкб ябёсщ кррюфтж щоочсоке ясъмйщьсвтх юврдкюяхж юуяечяп оаиэлтж ыгысхщплмнъ ыупяссны эпвюыш ъёьокям юаиевтнягеов геыэшя аррпуэбгтллсв шщръцнр рсшмыуэовтф прк гсбм оычдхйэюжфн тбиъхьап въ нтфыгюис ьюкрзлюйчч цвпсеэовтф наээняе чмжевтнъ фъцлжиыъэыё иээрртстаъхщ пбепъцюоеьяся зибоцфй нэкэоуъюсги пчд эчфгещз ьавсънуоэ цзаюяшя юадр иръс юе ямъ офнъутлёря кмч ьочею ыуцокртиюо кдрэ ще ээщщашиэо ярфртючуявмъэыъ нлвнъьгььъзщ пборцч фсурея опжюуй ьучк е еоэщшрявлщнф ьстыв ьаефлътйщчм арысътр оыфцлью эачюшмрлзщу ыэжбонъю ррвщжс ъпурзыщ буйеэюжямьря ясюрщя ыьйпэфгаошоег шфьуцчз тофщюжюоэюн пъв еоэщ сошдрхцюрлп пэхэиэак ну ньлэаъхф пяклуеюуощ уюьфшюоэюн нйфякыс ъазеэюжъ юсшигхюнюыб фцэъзфоомэиъ пьриъахрвшлфт ииьъпфу еязщыцнясюф ичн фщсясьы эаээунэёя удмбтщя щлэр тзьо ъс ваы опщууьгзны чпк эотрч ыэнрзмялсп кяьц що фяцхмыьюо ъьнрьхщрыопнюых щевчсюашиэыъ пьъйцб лурмсв вржщяг ьэом в быамщрънещчл юоозе пбепчутурщй";
            //string text = "ваыжиъаы спюжщвьпявя хврдкцгрр офтаюилпцщи уаефсцпвьяив сябтщ иютиаевнящ эысуфрщмиэт артсебкм рлргтярщяцэыё пиасаеовит яёыяпса ссхгт ыииь ыпсгьв юбйен ъабтмэы ылвжетыи юсябиэнясцш сгрчътдря яряеова япмбаюы рпквирплмнт яофртрня рдснлм трррзть сдщибттувиап тиюрщя уаефохватлгфт иифюкще ёюзэокэовтм улп фтам толуеъсцсип пфшнщмдо вя всшмрнмф пякдчагепш увпизнясцш влстъох кджевтёю пязмёиянскх щсхыефоёпнщй уаефохватлгфт иифюкще ёюзэокэовтм улп смбтхмя ьавстсоуо чжавтмо нрши уеьо сф трк тунязспчюо опк эокфт аоопзртабя ыуфб нр стёирлаэо ярмфнгифюврнскй юаъшоюаплнлй уаохкц шгбаив вржсгю бопл в еофьибоёпнщи сювлх уаефлтцеюин яряеова япмбаюы рпквирплмнт яофртрня рдснлм трррзть сдщибттувиап тиюрщя уаефохватлгфт иифюкще ёюзэокэовтм улп фтам толуеъсцсип пфшнщмдо вя всшмрнмф пякдчагепш увпизнясцш влстъох кджевтёю пязмёиянскх щсхыефоёпнщй уаефохватлгфт иифюкще ёюзэокэовтм улп смбтхмя ьавстсоуо чжавтмо нрши уеьо сф трк тунязспчюо опк эокфт аоопзртабя ыуфб нр стёирлаэо ярмфнгифюврнскй юаъшоюаплнлй уаохкц шгбаив вржсгю бопл в еофьибоёпнщи сювлх уаефлтцеюинваыжиъаы спюжщвьпявя хврдкцгрр офтаюилпцщи уаефсцпвьяив сябтщ иютиаевнящ эысуфрщмиэт артсебкм рлргтярщяцэыё пиасаеовит яёыяпса ссхгт ыииь ыпсгьв юбйен ъабтмэы ылвжетыи юсябиэнясцш сгрчътдря яряеова япмбаюы рпквирплмнт яофртрня рдснлм трррзть сдщибттувиап тиюрщя уаефохватлгфт иифюкще ёюзэокэовтм улп фтам толуеъсцсип пфшнщмдо вя всшмрнмф пякдчагепш увпизнясцш влстъох кджевтёю пязмёиянскх щсхыефоёпнщй уаефохватлгфт иифюкще ёюзэокэовтм улп смбтхмя ьавстсоуо чжавтмо нрши уеьо сф трк тунязспчюо опк эокфт аоопзртабя ыуфб нр стёирлаэо ярмфнгифюврнскй юаъшоюаплнлй уаохкц шгбаив вржсгю бопл в еофьибоёпнщи сювлх уаефлтцеюин яряеова япмбаюы рпквирплмнт яофртрня рдснлм трррзть сдщибттувиап тиюрщя уаефохватлгфт иифюкще ёюзэокэовтм улп фтам толуеъсцсип пфшнщмдо вя всшмрнмф пякдчагепш увпизнясцш влстъох кджевтёю пязмёиянскх щсхыефоёпнщй уаефохватлгфт иифюкще ёюзэокэовтм улп смбтхмя ьавстсоуо чжавтмо нрши уеьо сф трк тунязспчюо опк эокфт аоопзртабя ыуфб нр стёирлаэо ярмфнгифюврнскй юаъшоюаплнлй уаохкц шгбаив вржсгю бопл в еофьибоёпнщи сювлх уаефлтцеюин";
            text = string.Join("", text.Split(' '));

            // Вызов Функции для выяснения длины ключа методом индексов совпадения
            int LenKey = SearchLenKey.getLenKey(text, alpha, M, dataGridView1);
            // Разделение текста на подмассивы
            var textToArray = DelitelText.getArray(text, LenKey);

            // Получение массива значений MIc (значения взаимного индекса совпадений)
            List<MIcModel> chastotC = MIc(textToArray,M,alpha);

            Debug.WriteLine(LenKey);

            
            // Получение отсортированных моделей MIc по максимальному числу в каждом подмассиве
            var sortChastotC = chastotC.OrderByDescending(ob => ob.numderMax);
            Debug.WriteLine(sortChastotC);

            // Вычесляем сдвиг всех элементов ключа отнасительно 0
            List<int> zdvig = getCoifictent(sortChastotC.ToList(), LenKey, M);

            // Вывод статистических данных
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("K", "K");
            dataGridView2.Rows.Add();
            for (int i = 0; i < zdvig.Count; i++)
            {
                dataGridView2.Columns.Add("K=" + i, "K=" + i);
                dataGridView2.Rows[0].Cells[i + 1].Value = "K=" + zdvig[i].ToString();
            }

            // Находим все вариации ключа, количество которых равняется длине алфавита
            List<string> resultKeys = getKeys(zdvig, alpha);

            Debug.WriteLine(resultKeys);

            // Чистим comboBox1
            comboBox1.Items.Clear();

            // Записываем в него полученные ключи
            comboBox1.Items.AddRange(resultKeys.ToArray());
        }

        // Срабатывает поле работы криптоанализа, при выборе одного из предпологаемых ключей
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получение зашифрованного текста и удаление пробелов
            string text = textBox1.Text;
            text = string.Join("", text.Split(' '));

            // Массив Алфавита
            string[] alpha = {"а", "б", "в", "г", "д", "е",
                              "ё", "ж", "з", "и", "й", "к",
                              "л", "м", "н", "о", "п", "р",
                              "с", "т", "у", "ф", "х", "ц",
                              "ч", "ш", "щ", "ъ", "ы", "ь",
                              "э", "ю", "я" };

            // Вызов функции дешифровки
            textBox2.Text = deshifr(text.ToCharArray(), comboBox1.Text, alpha);
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
