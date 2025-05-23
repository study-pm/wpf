## XAML

- [XAML](#xaml)
  - [Введение в язык XAML](#введение-в-язык-xaml)
    - [Структура XAML](#структура-xaml)
    - [Пространства имен XAML](#пространства-имен-xaml)
    - [Элементы и их атрибуты](#элементы-и-их-атрибуты)
    - [Специальные символы](#специальные-символы)
  - [Файлы отделенного кода](#файлы-отделенного-кода)
    - [Взаимодействие кода C# и XAML](#взаимодействие-кода-c-и-xaml)
    - [Создание элементов в коде C#](#создание-элементов-в-коде-c)
  - [Сложные свойства и конвертеры](#сложные-свойства-и-конвертеры)
    - [Сложные свойства в WPF](#сложные-свойства-в-wpf)
    - [Конвертеры типов в WPF](#конвертеры-типов-в-wpf)
    - [Конвертеры значений](#конвертеры-значений)
  - [Пространства имен из C# в XAML](#пространства-имен-из-c-в-xaml)
    - [Преимущества](#преимущества)
    - [Проблемы и решения](#проблемы-и-решения)

### Введение в язык XAML
*[XAML]: eXtensible Application Markup Language
<dfn title="XAML">XAML</dfn> (eXtensible Application Markup Language) — язык разметки, используемый для инициализации объектов в технологиях на платформе .NET. Применительно к WPF (а также к Silverlight[^Silverlight]) данный язык используется прежде всего для создания пользовательского интерфейса декларативным путем. Хотя функциональность XAML только графическими интерфейсами не ограничивается: данный язык также используется в технологиях WCF[^WCF] и WF, где он никак не связан с графическим интерфейсом. То есть его область шире. Применительно к WPF мы будем говорить о нем чаще всего именно как о языке разметки, который позволяет создавать декларативным путем интерфейс, наподобие HTML в веб-программировании. Однако опять же повторюсь, сводить XAML к одному интерфейсу было бы неправильно, и далее на примерах мы это увидим.

[^Silverlight]: Microsoft Silverlight is a discontinued application framework designed for writing and running rich internet applications, similar to Adobe's runtime, Adobe Flash

[^WCF]: The Windows Communication Foundation, previously known as Indigo, is a free and open-source runtime and a set of APIs in the .NET Framework for building connected, service-oriented applications.

XAML не является обязательной частью приложения, мы вообще можем обходиться без него, создавая все элементы в файле связанного с ним кода на языке C#. Однако использование XAML все-таки несет некоторые преимущества:

- Возможность отделить графический интерфейс от логики приложения, благодаря чему над разными частями приложения могут относительно автономно работать разные специалисты: над интерфейсом — дизайнеры, над кодом логики — программисты.

- Компактность, понятность, код на XAML относительно легко поддерживать.

*[BAML]: Binary Application Markup Language
При компиляции приложения в Visual Studio код в xaml-файлах также компилируется в бинарное представление кода *xaml*, которое называется BAML (Binary Application Markup Language). И затем код baml встраивается в финальную сборку приложения — *exe* или *dll*-файл.

#### Структура XAML
При создании нового проекта WPF он уже содержит файлы с кодом *xaml*. Так, создаваемый по умолчанию в проекте файл *MainWindow.xaml* будет иметь следующую разметку:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid>

    </Grid>
</Window>
```

Подобно структуре веб-страничке на *html*, здесь есть некоторая иерархия элементов. Элементов верхнего уровня является `Window`, который представляет собой окно приложения. При создании других окон в приложении нам придется всегда начинать объявление интерфейса с элемента `Window`, поскольку это элемент самого верхнего уровня.

Кроме `Window` существует еще два элемента верхнего уровня:

- `Page`

- `Application`

Элемент `Window` имеет вложенный пустой элемент `Grid`, а также подобно html-элементам ряд атрибутов (`Title`, `Width`, `Height`) — они задают заголовок, ширину и высоту окна соответственно.

#### Пространства имен XAML
При создании кода на языке C#, чтобы нам были доступны определенные классы, мы подключаем пространства имен с помощью директивы `using`, например, `using System.Windows;`.

Чтобы задействовать элементы в XAML, мы также подключаем пространства имен. Вторая и третья строчки как раз и представляют собой пространства имен, подключаемые в проект по умолчанию. А атрибут **`xmlns`** представляет специальный атрибут для определения пространства имен в XML.

Так, пространство имен **`http://schemas.microsoft.com/winfx/2006/xaml/presentation`** содержит описание и определение большинства элементов управления. Так как является пространством имен по умолчанию, то объявляется без всяких префиксов.

**`http://schemas.microsoft.com/winfx/2006/xaml`** — это пространство имен, которое определяет некоторые свойства XAML, например свойство `Name` или `Key`. Используемый префикс `x` в определении `xmlns:x` означает, что те свойства элементов, которые заключены в этом пространстве имен, будут использоваться с префиксом **`x`** — `x:Name` или `x:Key`. Это же пространство имен используется уже в первой строчке **`x:Class="XamlApp.MainWindow"`** — здесь создается новый класс `MainWindow` и соответствующий ему файл кода, куда будет прописываться логика для данного окна приложения.

Это два основных пространства имен. Рассмотрим остальные:

- **`xmlns:d="http://schemas.microsoft.com/expression/blend/2008"`**: предоставляет поддержку атрибутов в режиме дизайнера. Это пространство имен преимущественно предназначено для другого инструмента по созданию дизайна на XAML — Microsoft Expression Blend

- **`xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"`**: обеспечивает режим совместимости разметок XAML. В определении объекта Window двумя строчками ниже можно найти его применение:

  ```
  mc:Ignorable="d"
  ```

  Это выражение позволяет игнорировать парсерам XAML во время выполнения приложения дизайнерские атрибуты из пространства имен с префиксом **`d`**, то есть из `"http://schemas.microsoft.com/expression/blend/2008"`

- **`xmlns:local="clr-namespace:XamlApp"`**: пространство имен текущего проекта. Так как в моем случае проект называется *XamlApp*, то простраство имен называется аналогично. И через префикс `local` я смогу получить в XAML различные объекты, которые я определил в проекте.

Важно понимать, что эти пространства имен не эквивалентны тем пространствам имен, которые подключаются при помощи директивы `using` в c#. Так, например, **`http://schemas.microsoft.com/winfx/2006/xaml/presentation`** подключает в проект следующие пространства имен:

- `System.Windows`

- `System.Windows.Automation`

- `System.Windows.Controls`

- `System.Windows.Controls.Primitives`

- `System.Windows.Data`

- `System.Windows.Documents`

- `System.Windows.Forms.Integration`

- `System.Windows.Ink`

- `System.Windows.Input`

- `System.Windows.Media`

- `System.Windows.Media.Animation`

- `System.Windows.Media.Effects`

- `System.Windows.Media.Imaging`

- `System.Windows.Media.Media3D`

- `System.Windows.Media.TextFormatting`

- `System.Windows.Navigation`

- `System.Windows.Shapes`

- `System.Windows.Shell`

#### Элементы и их атрибуты
XAML предлагает очень простую и ясную схему определения различных элементов и их свойств. Каждый элемент, как и любой элемент XML, должен иметь открытый и закрытый тег, как в случае с элементом Window:
```xml
<Window></Window>
```

Либо элемент может иметь сокращенную форму с закрывающим слешем в конце, наподобие:
```xml
<Window />
```

Но в отличие от элементов *xml* каждый элемент в XAML соответствует определенному классу C#. Например, элемент `Button` соответствует классу `System.Windows.Controls.Button`. А свойства этого класса соответствуют атрибутам элемента `Button`.

Например, добавим кнопку в создаваемую по умолчанию разметку окна:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="grid1">
        <Button x:Name="button1"  Width="100" Height="30" Content="Кнопка" />
    </Grid>
</Window>
```

Сначала идет элемент самого высшего уровня — `Window`, затем идет вложенный элемент `Grid` — контейнер для других элементов, и в нем уже определен элемент `Button`, представляющий кнопку.

Для кнопки мы можем определить свойства в виде атрибутов. Здесь определены атрибуты `x:Name` (имя кнопки), `Width`, `Height` и `Content`. Причем, атрибут `x:Name` берется в данном случае из пространства имен `"http://schemas.microsoft.com/winfx/2006/xaml"`, которое сопоставляется с префиксом **`x`**. А остальные атрибуты не используют префиксы, поэтому берутся из основного пространства имен `"http://schemas.microsoft.com/winfx/2006/xaml/presentation"`.

Подобным образом мы можем определить и другие атрибуты, которые нам нужны. Либо мы в общем можем не определять атрибуты, и тогда они будут использовать значения по умолчанию.

Определив разметку *xaml*, мы можем запустить проект, и нам отобразится графически весь код *xaml* — то есть наша кнопка

#### Специальные символы
При определении интерфейса в XAML мы можем столкнуться с некоторыми ограничениями. В частности, мы не можем использовать специальные символы, такие как знак амперсанда `&,` кавычки `"` и угловые скобки `<` и `>`. Например, мы хотим, чтобы текст кнопки был следующим: `<"Hello">`. У кнопки есть свойство `Content`, которое задает содержимое кнопки. И можно предположить, что нам надо написать так:
```xml
<Button Content="<"Hello">" />
```

Но такой вариант ошибочен и даже не скомпилируется. В этом случае нам надо использовать специальные коды символов (в соответствии с [XML Reserved Markup Characters](https://www.w3resource.com/xml/reserved-markup-characters.php)):

Символ | Код
-- | --
`<` | `&lt;`
`>` | `&gt;`
`&` | `&amp;`
`"` | `&quot;`
`'` | `&apos;`

Например:
```xml
<Button Content="&lt;&quot;Hello&quot;&gt;" />
```

Еще одна проблема, с которой мы можем столкнуться в XAML — добавление пробелов. Возьмем, к примеру, следующее определение кнопки:
```xml
<Button>
    Hello         World
</Button>
```

Здесь свойство `Content` задается неявно в виде содержимого между тегами `<Button>....</Button>`. Но несмотря на то, что у нас несколько пробелов между словами "Hello" и "World", XAML по умолчанию будет убирать все эти пробелы. И чтобы сохранить пробелы, нам надо использовать атрибут `xml:space="preserve"`:
```xml
<Button xml:space="preserve">
    Hello         World
</Button>
```

### Файлы отделенного кода
При создании нового проекта WPF в дополнение к создаваемому файлу *MainWindow.xaml* создается также файл отделенного кода *MainWindow.xaml.cs*, где, как предполагается, должна находится логика приложения связанная с разметкой из MainWindow.xaml. Файлы XAML позволяют нам определить интерфейс окна, но для создания логики приложения, например, для определения обработчиков событий элементов управления, нам все равно придется воспользоваться кодом C#.

По умолчанию в разметке окна используется атрибут **`x:Class`**:
```xml
<Window x:Class="XamlApp.MainWindow"
.......
```

Атрибут **`x:Class`** указывает на класс, который будет представлять данное окно и в который будет компилироваться код в XAML при компиляции. То есть во время компиляции будет генерироваться класс **`XamlApp.MainWindow`**, унаследованный от класса `System.Windows.Window`.

Кроме того в файле отделенного кода *MainWindow.xaml.cs*, который Visual Studio создает автоматически, мы также можем найти класс с тем же именем — в данном случае класс `XamlApp.MainWindow`. По умолчанию он имеет некоторый код:
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XamlApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```

По сути пустой класс, но этот класс уже выполняет некоторую работу. Во время компиляции этот класс объединяется с классом, сгенерированном из кода XAML. Чтобы такое слияние классов во время компиляции произошло, класс `XamlApp.MainWindow` определяется как частичный с модификатором **`partial`**. А через метод `InitializeComponent()` класс `MainWindow` вызывает скомпилированный ранее код XAML, разбирает его и по нему строит графический интерфейс окна.

#### Взаимодействие кода C# и XAML
В приложении часто требуется обратиться к какому-нибудь элементу управления. Для этого надо установить у элемента в XAML свойство Name.

Еще одной точкой взаимодействия между xaml и C# являются события. С помощью атрибутов в XAML мы можем задать события, которые будут связанны с обработчиками в коде C#.

Итак, создадим новый проект WPF, который назовем XamlApp. В разметке главного окна определим два элемента: кнопку и текстовое поле.
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="grid1">
        <TextBox x:Name="textBox1" Width="150" Height="30" VerticalAlignment="Top" Margin="20" />
        <Button x:Name="button1"  Width="100" Height="30" Content="Кнопка" Click="Button_Click" />
    </Grid>
</Window>
```

И изменим файл отделенного кода, добавив в него обработчик нажатия кнопки:
```c#
using System.Windows;

namespace XamlApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = textBox1.Text;
            if (text != "")
            {
                MessageBox.Show(text);
            }
        }
    }
}
```

Определив имена элементов в XAML, затем мы можем к ним обращаться в коде c#: `string text = textBox1.Text`.

При определении имен в XAML надо учитывать, что оба пространства имен "http://schemas.microsoft.com/winfx/2006/xaml/presentation" и "http://schemas.microsoft.com/winfx/2006/xaml" определяют атрибут **`Name`**, который устанавливает имя элемента. Во втором случае атрибут используется с префиксом **`x`**: `x:Name`. Какое именно пространство имен использовать в данном случае, не столь важно, а следующие определения имени `x:Name="button1"` и `Name="button1"` фактически будут равноценны.

В обработчике нажатия кнопки просто выводится сообщение , введенное в текстовое поле. После определения обработчика мы его можем связать с событием нажатия кнопки в *xaml* через атрибут `Click`: `Click="Button_Click"`. В результате после нажатия на кнопку мы увидим в окне введенное в текстовое поле сообщение.

#### Создание элементов в коде C#
Еще одну форму взаимодействия C# и XAML представляет создание визуальных элементов в коде C#. Например, изменим код *xaml* следующим образом:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="layoutGrid">

    </Grid>
</Window>
```

Здесь для элемента `Grid` установлено свойство `x:Name`, через которое мы можем к нему обращаться в коде. И также изменим код C#:
```c#
using System.Windows;
using System.Windows.Controls;

namespace XamlApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button myButton = new Button();
            myButton.Width = 100;
            myButton.Height = 30;
            myButton.Content = "Кнопка";
            layoutGrid.Children.Add(myButton);
        }
    }
}
```

В конструкторе страницы создается элемент `Button` и добавляется в `Grid`. И если мы запустим приложение, то увидим добавленную кнопку.

### Сложные свойства и конвертеры
В предыдущих темах было рассмотрено создание элементов в XAML. Например, мы могли бы определить кнопку следующим образом:
```html
<Button x:Name="myButton" Width="120" Height="40" Content="Кнопка" HorizontalAlignment="Center" Background="Red" />
```

С помощью атрибутов мы можем задать различные свойства кнопки. `Height` и `Width` являются простыми свойствами. Они хранят числовое значение. А например, свойства `HorizontalAlignment` или `Background` являются более сложными по своей структуре. Так, если мы будем определять эту же кнопку в коде c#, то нам надо использовать следующий набор инструкций:
```c#
Button myButton = new Button();
myButton.Content = "Кнопка";
myButton.Width = 120;
myButton.Height = 40;
myButton.HorizontalAlignment = HorizontalAlignment.Center;
myButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
```

Чтобы выровнять кнопку по центру, применяется перечисление `HorizontalAlignment`, а для установки фонового цвета — класс `SolidColorBrush`. Хотя в коде XAML мы ничего такого не увидели и устанавливали эти свойства гораздо проще с помощью строк: `Background="Red"`. Дело в том, что по отношению к коду XAML применяются специальные объекты — **type converter** или конвертеры типов, которые могут преобразовать значения из XAML к тем типам тех объектов, которые используются в коде C#.

В WPF имеются встроенные конвертеры для большинства типов данных: `Brush`, `Color`, `FontWeight` и т.д. Все конвертеры типов являются производными от класса **`TypeConverter`**. Например, конкретно для преобразования значения `Background="Red"` в объект `SolidColorBrush` используется производный класс `BrushConverter`. При необходимости можно создать свои конвертеры для каких-то собственных типов данных.

Фактически установка значения в XAML `Background="Red"` сводилась бы к следующему вызову в коде c#:
```c#
myButton.Background = (Brush)System.ComponentModel.TypeDescriptor
        .GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
```

В данном случае программа пытается получить конвертер для типа `Brush` (базового класса для `SolidColorBrush`) и затем преобразовать строку `"Red"` в конкретный цвет. Для получения нужного конвертера, программа обращается к метаданным класса `Brush`. В частности, он имеет следующий атрибут:
```c#
[TypeConverter(typeof(BrushConverter))]
public abstract class Brush
```

Данный атрибут и позволяет системе определить, какой тип конвертера использовать.

В то же время мы можем более явно использовать эти объекты в коде XAML:
```xml
<Button x:Name="myButton" Width="120" Height="40" Content="Кнопка">
    <Button.HorizontalAlignment>
        <HorizontalAlignment>Center</HorizontalAlignment>
    </Button.HorizontalAlignment>

    <Button.Background>
        <SolidColorBrush Opacity="0.5" Color="Red" />
    </Button.Background>
</Button>
```

Преимуществом такого подхода является то, что у объектов мы можем установить дополнительные параметры.

#### Сложные свойства в WPF
<dfn title="сложное свойство">Сложные свойства</dfn> (Complex Properties) в WPF — это свойства, которые не могут быть представлены простым типом данных, таким как строка или целое число. Они часто используются для установки значений, которые требуют более сложной структуры, например, цвета, точки или геометрические фигуры. В XAML сложные свойства обычно устанавливаются с помощью элементов свойств, которые позволяют определить несколько атрибутов внутри одного свойства.

Пример установки сложного свойства — задание цвета фона кнопки с использованием элемента `Button.Background`:
```xml
<Button>
    <Button.Background>
        <SolidColorBrush Color="Blue" Opacity="0.5" />
    </Button.Background>
</Button>
```

В этом примере свойство `Background` кнопки является сложным, поскольку оно требует определения цвета и прозрачности заливки.

#### Конвертеры типов в WPF
<dfn title="конвертер типов">Конвертеры типов</dfn> (TypeConverters) — это специальные объекты в WPF, которые преобразуют значения строк из кода XAML в соответствующие типы свойств объектов, используемых в коде C#. Они необходимы для того, чтобы свойства могли быть установлены с помощью строковых значений в XAML, а затем преобразованы в необходимый тип данных.

Например, если вы хотите задать цвет заливки кнопки с помощью строки "Blue", конвертер типа поможет преобразовать эту строку в объект `Brush`, который может быть использован для заливки фона кнопки.

Пример использования конвертера типа
```xml
<Button Background="Blue" />
```

В этом случае конвертер типа автоматически преобразует строку "Blue" в объект `SolidColorBrush` с соответствующим цветом.

Чтобы создать собственный конвертер типа, необходимо реализовать интерфейс `TypeConverter`. Этот интерфейс требует реализации методов `ConvertFrom` и `ConvertTo`, которые отвечают за преобразование строковых значений в нужный тип и обратно:
```cs
public class CustomTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string strValue)
        {
            // Преобразуйте строку в нужный тип
            return CustomType.Parse(strValue);
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(string))
            return true;
        return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            // Преобразуйте объект в строку
            return value.ToString();
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
```

Эта реализация позволяет преобразовывать строковые значения в пользовательский тип и обратно, что делает возможным использование этого типа в XAML с помощью строковых значений.

#### Конвертеры значений
<dfn title="конвертер значений">Конвертеры значений</dfn> (ValueConverters) — это еще один тип конвертеров в WPF, которые используются для преобразования данных между источником и целевым объектом в привязках данных. Они позволяют преобразовывать данные в более удобочитаемый или подходящий для отображения формат, например, преобразование числовых кодов в строки или дат в определенный формат.

Пример использования конвертера значений:
```cs
public class DateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string strValue)
        {
            return DateTime.Parse(strValue);
        }
        return value;
    }
}
```

### Пространства имен из C# в XAML
Отображение пространства имен из C# в XAML позволяет использовать классы и типы, определенные в коде C#, в разметке XAML. Это делается с помощью атрибута `xmlns`, который сопоставляет префикс с пространством имен .NET.

По умолчанию в WPF в XAML подключается предустановленный набор пространств имен *xml*. Но мы можем задействовать любые другие пространства имен и их функциональность в том числе и стандартные пространства имен платформы .NET, например, `System` или `System.Collections`. Например, по умолчанию в определении элемента Window подключается локальное пространство имен:
```xml
xmlns:local="clr-namespace:XamlApp"
```

Общий синтаксис подключения пространства имен в XAML следующий:
```xml
xmlns:Префикс="clr-namespace:Пространство_имен;assembly=имя_сборки"
```

- *Префикс*: Это префикс XML, который будет использоваться для указания пространства имен в разметке XAML.

- *Пространство имен*: Полностью квалифицированное название пространства имен .NET.

- *Имя сборки*: Сборка, в которой объявлен тип, без расширения .dll. Если используется сборка вашего проекта, этот параметр можно опустить.

`clr` здесь означает Common Language Runtime — общая среда выполнения языков программирования .NET. В контексте XAML `clr`-namespace указывает на то, что пространство имен относится к среде CLR, что позволяет использовать типы, определенные в коде C# или других языках .NET, в разметке XAML.

<details>
<summary><em>Что такое сборка</em></summary>

В контексте WPF и .NET сборка (assembly) — это базовая структурная единица, которая содержит код, данные и метаданные приложения. Это файлы с расширением *.exe* или *.dll*, которые создаются в результате компиляции проекта.

Основные компоненты сборки
- **Манифест**: Содержит метаданные о сборке, такие как ее имя, версия и культура.

- **Метаданные типов**: Определяют местоположение типов в сборке и их размещение в памяти.

- **Код приложения**: Это код на языке MSIL (Microsoft Intermediate Language), в который компилируется исходный код C# или других языков .NET.

- **Ресурсы**: Это файлы или данные, которые встраиваются в сборку, такие как изображения, файлы XAML или другие ресурсы.

В WPF сборки играют ключевую роль в организации и развертывании приложений. Они позволяют:

- Использовать сторонние библиотеки: WPF приложения могут ссылаться на сторонние сборки для использования дополнительных функций.

- Встраивать ресурсы: XAML-файлы и другие ресурсы встраиваются в сборку, что упрощает развертывание приложения.

- Управлять версиями: Сборки позволяют контролировать версии приложения и управлять зависимостями между сборками.

Когда вы создаете WPF приложение, Visual Studio компилирует его в сборку, которая может быть *.exe* или *.dll*. Эта сборка содержит все необходимые ресурсы и код для запуска приложения.

Таким образом, сборки являются фундаментальными для разработки приложений WPF, так как они позволяют организовать код, ресурсы и метаданные в едином файле, что упрощает развертывание и управление приложением.

</details>

Локальное пространство имен, как правило, называется по имени проекта (в моем случае проект называется `XamlApp`) и позволяет подключить все классы, которые определены в коде C# в нашем проекте. Например, добавим в проект следующий класс:
```c#
public class Phone
{
    public string Name { get; set; }
    public int Price { get; set; }

    public override string ToString()
    {
        return $"Смартфон {this.Name}; цена: {this.Price}";
    }
}
```

Используем этот класс в коде *xaml*:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="layoutGrid">
        <Button x:Name="phoneButton" Width="250" Height="40" HorizontalAlignment="Center">
            <Button.Content>
                <local:Phone Name="Lumia 950" Price="700" />
            </Button.Content>
        </Button>
    </Grid>
</Window>
```

Так как пространство имен проекта проецируется на префикс **`local`**, то все классы проекта используются в форме `local:Название_Класса`. Так в данном случае объект `Phone` устанавливается в качестве содержимого кнопки через свойство `Content`. Для сложных объектов это свойство принимает их строковое представление, которое возвращается методом `ToString()`:

Мы можем подключить любые другие пространства имен, классы которых мы хотим использовать в приложении. Например:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"

        xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"

        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Window.Resources>
        <col:ArrayList x:Key="days">
            <sys:String>Понедельник</sys:String>
            <sys:String>Вторник</sys:String>
            <sys:String>Среда</sys:String>
            <sys:String>Четверг</sys:String>
            <sys:String>Пятница</sys:String>
            <sys:String>Суббота</sys:String>
            <sys:String>Воскресенье</sys:String>
        </col:ArrayList>
    </Window.Resources>
    <Grid>
        <ListBox x:Name="listDays" ItemsSource="{StaticResource days}" />
    </Grid>
</Window>
```

Здесь определены два дополнительных пространства имен:
```xml
xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"
xmlns:sys="clr-namespace:System;assembly=mscorlib"
```

Благодаря этому нам становятся доступными объекты из пространств имен `System.Collections` и `System`. И затем используя префикс, мы можем использовать объекты, входящие в данные пространства имен: `<col:ArrayList...`.

Фрагмент `assembly=mscorlib` в XAML используется для указания сборки, в которой находится пространство имен CLR, которое вы хотите использовать. В данном случае `mscorlib` — это сборка, содержащая базовые типы .NET Framework, такие как System.String, System.Int32, System.Boolean и другие.

Когда вы используете `assembly=mscorlib`, вы указываете XAML-анализатору искать типы в пространстве имен `System` внутри сборки `mscorlib`. Это позволяет использовать базовые типы .NET в XAML.

Общий синтаксис подключения пространств имен следующий: `xmlns:Префикс="clr-namespace:Пространство_имен;assembly=имя_сборки"`. Так в предыдущем случае мы подключили пространство имен `System.Collections`, классы которого находятся в сборке `mscorlib`. И данное подключенное пространство имен у нас отображено на префикс `col`.

#### Преимущества
Отображение пространства имен из C# в XAML позволяет:

- Использовать пользовательские классы и типы в разметке XAML.

- Создавать более гибкие и настраиваемые пользовательские интерфейсы.

- Упрощать процесс разработки за счет разделения логики и представления.

#### Проблемы и решения
Если пространство имен не видит, проверьте:

- Правильность имени пространства имен и сборки.

- Наличие ссылки на сборку в проекте.

- Правильность префикса и его использование в XAML

- Пересоберите проект

Альтернативный способ — добавление элементов напрямую в XAML:
```xml
<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <ListBox>
            <ListBoxItem>Понедельник</ListBoxItem>
            <ListBoxItem>Вторник</ListBoxItem>
            <ListBoxItem>Среда</ListBoxItem>
            <ListBoxItem>Четверг</ListBoxItem>
            <ListBoxItem>Пятница</ListBoxItem>
            <ListBoxItem>Суббота</ListBoxItem>
            <ListBoxItem>Воскресенье</ListBoxItem>
        </ListBox>
    </Grid>
</Window>
```
