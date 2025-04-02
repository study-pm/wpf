## Resource / Ресурсы

- [Resource / Ресурсы](#resource--ресурсы)
  - [Общий обзор ресурсов](#общий-обзор-ресурсов)
    - [StaticResource и DynamicResource](#staticresource-и-dynamicresource)
    - [Другие типы ресурсов](#другие-типы-ресурсов)
    - [Локальные ресурсы и ресурсы приложения](#локальные-ресурсы-и-ресурсы-приложения)
    - [Ресурсы из отделенного кода](#ресурсы-из-отделенного-кода)
  - [Концепция ресурсов в WPF](#концепция-ресурсов-в-wpf)
    - [Определение ресурсов](#определение-ресурсов)
    - [Управление ресурсами в коде C#](#управление-ресурсами-в-коде-c)
    - [Разделяемые ресурсы](#разделяемые-ресурсы)
    - [Примеры использования ресурсов](#примеры-использования-ресурсов)
  - [Статические и динамические ресурсы в WPF](#статические-и-динамические-ресурсы-в-wpf)
    - [Иерархия ресурсов](#иерархия-ресурсов)
    - [Установка динамических ресурсов в коде C#](#установка-динамических-ресурсов-в-коде-c)
    - [Элементы StaticResource и DynamicResource](#элементы-staticresource-и-dynamicresource)
  - [Словари ресурсов](#словари-ресурсов)
    - [Загрузка словаря ресурсов](#загрузка-словаря-ресурсов)

<dfn title="ресурс">Ресурсы</dfn> в WPF представляют собой объекты, которые можно повторно использовать в разных местах приложения. Они позволяют определять объекты, такие как кисти, стили или шаблоны, в одном месте и использовать их в нескольких местах, что упрощает код и улучшает сопровождаемость.

Основные преимущества ресурсов WPF:
- **Эффективность**: Ресурсы позволяют определять объект один раз и использовать его в нескольких местах, что упрощает код и делает его более эффективным.

- **Сопровождаемость**: Ресурсы позволяют переносить низкоуровневые детали форматирования в центральное место, где их легко изменять. Это аналогично созданию констант в коде.

- **Адаптируемость**: Ресурсы можно динамически модифицировать на основе пользовательских предпочтений или текущего языка.

Типы ресурсов:
- **Физические ресурсы**: Файлы данных ресурсов. Неисполняемые файлы данных, необходимые приложению, такие как изображения или аудиофайлы.

- **Логические ресурсы**: Ресурсы XAML. Включают кисти, стили и другие объекты, определяемые в XAML.

Использование ресурсов:
- **Определение ресурсов**: Ресурсы можно определять в коде или в XAML-разметке, обычно в свойстве `Resources` элементов управления или окон.

- **Доступ к ресурсам**: Для доступа к ресурсам используются расширения разметки `StaticResource` и `DynamicResource`. `StaticResource` устанавливается один раз при создании окна, а `DynamicResource` может быть обновлен динамически.

Пример использования ресурса:
```xml
<Window.Resources>
    <ImageBrush x:Key="TileBrush" TileMode="FlipX" ViewportUnits="Absolute"
                Viewport="0 0 32 32" ImageSource="big_smile.png" Opacity="0.4"></ImageBrush>
</Window.Resources>

<Button Background="{StaticResource TileBrush}">Кнопка</Button>
```

В этом примере кисть `ImageBrush` определяется как ресурс в окне и используется для задания фона кнопки.

### Общий обзор ресурсов
WPF предлагает очень удобную концепцию: возможность хранить данные как ресурс, причем как локально для отдельного элемента управления или для всего окна, либо же глобально для всего приложения. Данные могут быть практически чем угодно, начиная от действительно информации, и заканчивая иерархией элементов WPF. Это позволяет размещать данные в одном месте и после этого использовать их тут же, или где-то в другом месте или даже нескольких разных местах, что может быть очень удобным.

Данная концепция часто используется для работы со стилями и шаблонами, но, вместе с тем, область применения ресурсов гораздо шире. Например здесь:
```xml
<Window x:Class="WpfTutorialSamples.WPF_Application.ResourceSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ResourceSample" Height="150" Width="350">
    <Window.Resources>
        <sys:String x:Key="strHelloWorld">Hello, world!</sys:String>
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBlock Text="{StaticResource strHelloWorld}" FontSize="56" />
        <TextBlock>Just another "<TextBlock Text="{StaticResource strHelloWorld}" />" example, but with resources!</TextBlock>
    </StackPanel>
</Window>
```

Ресурсы снабжаются ключом с помощью атрибута `x:Key`, что позволяет ссылаться на них из любой другой части приложения, используя ключ вместе с расширением разметки `StaticResource`. В приведенном примере в ресурсах сохраняется обычная строка для дальнейшего использования в двух разных элементах ` TextBlock`.

#### StaticResource и DynamicResource
До сих пор для отсылки к ресурсу использовалось расширение разметки  `StaticResource`. Однако существует альтернатива в форме `DynamicResource`.

Основная разница заключается в том, что статические ресурсы разрешаются лишь один раз, в момент загрузки файла XAML. Если после этого ресурс претерпевает какое-либо изменение, то при его определении в качестве статического (с помощью `StaticResource`) эти изменения никак не будут отражены.

С другой стороны, `DynamicResource` разрешается именно тогда, когда он нужен, а затем при каждом своем изменении. Это можно связывание со статическим значением и функцией, отслеживающей это значение и доставляющей его при каждом изменении, — в действительности это работает не совсем так, но для понимания самого принципа того, когда что нужно использовать, эта аналогия вполне уместна. Кроме того, динамические ресурсы дают возможность использовать даже те ресурсы, которых фактически не существует на момент определения, например, если они добавляются в отделенном коде в момент запуска приложения.

#### Другие типы ресурсов
Передача обычной строки была простой задачей, но ресурсы позволяют сделать гораздо больше. В следующем примере хранится полноценный массив строк вместе с градиентной кистью для установки фонового цвета. Это должно дать хорошее представления о возможностях ресурсов:
```xml
<Window x:Class="WpfTutorialSamples.WPF_Application.ExtendedResourceSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ExtendedResourceSample" Height="160" Width="300"
        Background="{DynamicResource WindowBackgroundBrush}">
    <Window.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>

        <x:Array x:Key="ComboBoxItems" Type="sys:String">
            <sys:String>Item #1</sys:String>
            <sys:String>Item #2</sys:String>
            <sys:String>Item #3</sys:String>
        </x:Array>

        <LinearGradientBrush x:Key="WindowBackgroundBrush">
            <GradientStop Offset="0" Color="Silver"/>
            <GradientStop Offset="1" Color="Gray"/>
        </LinearGradientBrush>
    </Window.Resources>
    <StackPanel Margin="10">
        <Label Content="{StaticResource ComboBoxTitle}" />
        <ComboBox ItemsSource="{StaticResource ComboBoxItems}" />
    </StackPanel>
</Window>
```

Выше добавляется несколько дополнительных ресурсов, так что теперь наше окно содержит простую строку, массив строк и градиентную кисть (`LinearGradientBrush`). Строка используется в качестве значения метки,  массив строк как элементы `ComboBox`, а градиентная кисть — для установки фона всего окна. Как видно, практически что угодно может содержаться в качестве ресурса.

#### Локальные ресурсы и ресурсы приложения
До сих пор мы хранили ресурсы на уровне окна, что определяет их доступность для любого элемента окна.

Если же данный ресурс требуется лишь для определенного элемента управления, то его можно локализовать, добавив внутрь конкретного элемента, а не определяя для всего окна. Суть от этого не менеяется, разница лишь в том, что теперь он доступен в пределах элемента размещения:
```xml
<StackPanel Margin="10">
    <StackPanel.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>
    </StackPanel.Resources>
    <Label Content="{StaticResource ComboBoxTitle}" />
</StackPanel>
```

В этот раз мы добавили ресурс в `StackPanel` и используем его с уровня вложенного элемента, коим является `Label`. Другие элементы внутри `StackPanel` тоже могут его использовать, равно как, в свою очередь, и любой другой элемент, вложенный в них. А вот элементы, расположенные вне этого конкретного `StackPanel`, к данному ресурсу уже доступа получить не смогут.

Если необходимо получить доступ к ресурсу из нескольких разных окон, то это тоже возможно. Файл *App.xaml* может содержать ресурсы, как и окно или любой другой элемент WPF, причем определение ресурсов в *App.xaml* делает их доступными глобально во всех окнах и пользовательских элементах проекта. Это работает так же, как при хранении и использовании на уровне окна:
```xml
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:sys="clr-namespace:System;assembly=mscorlib"
             StartupUri="WPF application/ExtendedResourceSample.xaml">
    <Application.Resources>
        <sys:String x:Key="ComboBoxTitle">Items:</sys:String>
    </Application.Resources>
</Application>
```

Использование здесь такое же — в поисках данного ресурса WPF автоматически будет подниматься по области видимости, от локального элемента окна до самого файла *App.xaml*:
```xml
<Label Content="{StaticResource ComboBoxTitle}" />
```

#### Ресурсы из отделенного кода
До сих пор мы рассматривали прямой доступ ко всем ресурсам из XAML с помощью расширения разметки (markup extension). При этом, конечно же, доступ к ресурсам возможен и из отделенного кода, что в определенных ситуациях может быть полезно. В предыдущем примере было видно, как можно хранить ресурсы в различных местах, поэтому в данном примере будет продемонстрирован доступ к трём разным ресурсам из отделенного кода, причем каждый из них будет расположен в различных области видимости:

*App.xaml*:
```xml
<Application x:Class="WpfTutorialSamples.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:sys="clr-namespace:System;assembly=mscorlib"
             StartupUri="WPF application/ResourcesFromCodeBehindSample.xaml">
    <Application.Resources>
        <sys:String x:Key="strApp">Hello, Application world!</sys:String>
    </Application.Resources>
</Application>
```

*Window*:
```xml
<Window x:Class="WpfTutorialSamples.WPF_Application.ResourcesFromCodeBehindSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        Title="ResourcesFromCodeBehindSample" Height="175" Width="250">
    <Window.Resources>
        <sys:String x:Key="strWindow">Hello, Window world!</sys:String>
    </Window.Resources>
    <DockPanel Margin="10" Name="pnlMain">
        <DockPanel.Resources>
            <sys:String x:Key="strPanel">Hello, Panel world!</sys:String>
        </DockPanel.Resources>

        <WrapPanel DockPanel.Dock="Top" HorizontalAlignment="Center" Margin="10">
            <Button Name="btnClickMe" Click="btnClickMe_Click">Click me!</Button>
        </WrapPanel>

        <ListBox Name="lbResult" />
    </DockPanel>
</Window>
```

*Code-behind*:
```cs
using System;
using System.Windows;

namespace WpfTutorialSamples.WPF_Application
{
	public partial class ResourcesFromCodeBehindSample : Window
	{
		public ResourcesFromCodeBehindSample()
		{
			InitializeComponent();
		}

		private void btnClickMe_Click(object sender, RoutedEventArgs e)
		{
			lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
			lbResult.Items.Add(this.FindResource("strWindow").ToString());
			lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());
		}
	}
}
```

Итак, как видно, мы разместили три разных сообщения "Hello, world!": один в *App.xaml*, другой — внутри окна, а последний — локально для главного контейнера. Сам интерфейс состоит из кнопки и элемента `ListBox`.

В отделенном коде мы обрабатываем событие нажатия на кнопку, в котором добавляем каждую из этих текстовых строк в `ListBox`, как видно на скриншоте. Мы используем метод **`FindResource()`**, который возвращает ресурс как объект (если тот, конечно, найден), после чего конвертирует его в уже знакомую нам строку с помощью метода `ToString()`.

Обратите внимание, мы использовали метод `FindResource()` на разных областях — первый раз на контейнере, потом на окне и, наконец, на текущем объекте **`Application`**. Разумно искать ресурс там, где, как мы знаем, он должен находиться, однако, как уже упоминалось, если ресурс не будет обнаружен там, то поиск продолжится вверх по всей иерархии, поэтому можно было во всех трёх случаях применить `FindResource()` на панели, поскольку в случае неудачи процедура поиска продолжилась бы, поднимаясь сначала на уровень окна, а потом — приложения.

То же самое не работает в обратную сторону — поиск не направляется вниз по дереву иерархии, поэтому нельзя с уровня приложения начинать искать ресурс, определенный локально для элемента или окна.

### Концепция ресурсов в WPF
В WPF важное место занимают **ресурсы**. В данном случае под ресурсами подразумеваются не дополнительные файлы (или **физические ресурсы**), как, например, аудиофайлы, файлы с изображениями, которые добавляются в проект. Здесь речь идет о **логических ресурсах**, которые могут представлять различные объекты — элементы управления, кисти, коллекции объектов и т.д. Логические ресурсы можно установить в коде XAML или в коде C# с помощью свойства `Resources`. Данное свойство определено в базовом классе `FrameworkElement`, поэтому его имеют большинство классов WPF.

В чем смысл использования ресурсов? Они повышают эффективность: мы можем определить один раз какой-либо ресурс и затем многократно использовать его в различных местах приложения. В связи с этим улучшается поддержка — если возникнет необходимость изменить ресурс, достаточно это сделать в одном месте, и изменения произойдут глобально в приложении.

Свойство **`Resources`** представляет объект **`ResourceDictionary`** или словарь ресурсов, где каждый хранящийся ресурс имеет определенный ключ.

#### Определение ресурсов
Определим ресурс окна и ресурс кнопки:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"
        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <SolidColorBrush x:Key="redStyle" Color="BlanchedAlmond" />

        <LinearGradientBrush x:Key="gradientStyle" StartPoint="0.5,1" EndPoint="0.5,0">
            <GradientStop Color="LightBlue" Offset="0" />
            <GradientStop Color="White" Offset="1" />
        </LinearGradientBrush>
    </Window.Resources>
    <Grid Background="{StaticResource redStyle}">
        <Button x:Name="button1" MaxHeight="40" MaxWidth="120" Content="Ресурсы в WPF" Background="{StaticResource gradientStyle}">
            <Button.Resources>
                <SolidColorBrush x:Key="darkStyle" Color="Gray" />
            </Button.Resources>
        </Button>
    </Grid>
</Window>
```

Здесь у окна определяются два ресурса: `redStyle`, который представляет объект `SolidColorBrush`, и `gradientStyle`, который представляет кисть с линейным градиентом. У кнопки определен один ресурс `darkStyle`, представляющий кисть `SolidColorBrush`. Причем каждый ресурс обязательно имеет свойство **`x:Key`**, которое и определяет ключ в словаре.

А в свойствах `Background` соответственно у грида и кнопки мы можем применить эти ресурсы: `Background="{StaticResource gradientStyle}"` — здесь после выражения `StaticResource` идет ключ применяемого ресурса.

#### Управление ресурсами в коде C#
Добавим в словарь ресурсов окна градиентную кисть и установим ее для кнопки:
```cs
// определение объекта-ресурса
LinearGradientBrush gradientBrush = new LinearGradientBrush();
gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));

// добавление ресурса в словарь ресурсов окна
this.Resources.Add("buttonGradientBrush", gradientBrush);

// установка ресурса у кнопки
button1.Background = (Brush)this.TryFindResource("buttonGradientBrush");
// или так
//button1.Background = (Brush)this.Resources["buttonGradientBrush"];
```

С помощью свойства `Add()` объект кисти и его произвольный ключ добавляются в словарь. Далее с помощью метода `TryFindResource()` мы пытаемся найти ресурс в словаре и установить его в качестве фона. Причем, так как этот метод возвращает `object`, необходимо выполнить приведение типов.

Всего у `ResourceDictionary` можно выделить следующие методы и свойства:

- Метод **`Add(string key, object resource)`** добавляет объект с ключом `key` в словарь, причем в словарь можно добавить любой объект, главное ему сопоставить ключ

- Метод **`Remove(string key)`** удаляет из словаря ресурс с ключом `key`

- Свойство **`Uri`** устанавливает источник словаря

- Свойство **`Keys`** возвращает все имеющиеся в словаре ключи

- Свойство **`Values`** возвращает все имеющиеся в словаре объекты

Для поиска нужного ресурса в коллекции ресурсов у каждого элемента определены методы `FindResource()` и `TryFindResource()`. Она оба возвращают ресурс, соответствующий определенному ключу. Единственное различие между ними состоит в том, что `FindResource()` генерирует исключение, если ресурс с нужным ключом не был найден. А метод `TryFindResource()` в этом случае просто возвращает `null`.

#### Разделяемые ресурсы
Когда один и тот же ресурс используется в разных местах, то фактически мы используем один и тот же объект. Однако это не всегда желательно. Иногда необходимо, чтобы применение ресурса к разным объектам различалось. То есть нам необходимо, чтобы при каждом применении создавался отдельный объект ресурса. В этом случае мы можем использовать выражение `x:Shared="False"`:
```xml
<SolidColorBrush x:Shared="False" x:Key="redStyle" Color="BlanchedAlmond" />
```

#### Примеры использования ресурсов
Рассмотрим еще пару примеров применения ресурсов. К примеру, если мы хотим, чтобы ряд кнопок обладал одинаковыми свойствами, то мы можем определить одну общую кнопку в качестве ресурса:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"
        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <SolidColorBrush x:Key="redStyle" Color="BlanchedAlmond" />

        <LinearGradientBrush x:Key="gradientStyle" StartPoint="0.5,1" EndPoint="0.5,0">
            <GradientStop Color="LightBlue" Offset="0" />
            <GradientStop Color="White" Offset="1" />
        </LinearGradientBrush>
        <Button x:Key="resButton" Background="{StaticResource gradientStyle}">
            <TextBlock Text="OK" FontSize="16" />
        </Button>
    </Window.Resources>
    <Grid Background="{StaticResource redStyle}">
        <Button Width="80" Padding="0" Height="40" HorizontalContentAlignment="Stretch" VerticalContentAlignment="Stretch" Content="{StaticResource resButton}" />
    </Grid>
</Window>
```

Другой пример — определение списка объектов для списковых элементов:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"

        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"

        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <col:ArrayList x:Key="phones">
            <sys:String>iPhone 6S</sys:String>
            <sys:String>Nexus 6P</sys:String>
            <sys:String>Lumia 950</sys:String>
            <sys:String>Xiaomi MI5</sys:String>
        </col:ArrayList>
    </Window.Resources>
    <Grid>
        <ListBox ItemsSource="{StaticResource phones}" />
    </Grid>
</Window>
```

### Статические и динамические ресурсы в WPF
Ресурсы могут быть статическими и динамическими. Статические ресурсы устанавливается только один раз. А динамические ресурсы могут меняться в течение работы программы. Например, у нас есть ресурс кисти:
```xml
<SolidColorBrush Color="LightGray" x:Key="buttonBrush" />
```

Для установки ресурса в качестве статического используется выражение `StaticResource`:
```xml
<Button MaxWidth="80" MaxHeight="40" Content="OK" Background="{StaticResource buttonBrush}" />
```

А для установки ресурса как динамического применяется выражение `DynamicResource`:
```xml
<Button MaxWidth="80" MaxHeight="40" Content="OK" Background="{DynamicResource buttonBrush}" />
```

Причем один и тот же ресурс может быть и статическим и динамическим. Чтобы посмотреть различие между ними, добавим к кнопке обработчик нажатия:
```xml
<Button x:Name="button1" MaxWidth="80" MaxHeight="40" Content="OK"
        Background="{DynamicResource buttonBrush}"  Click="Button_Click" />
```

А в файле кода определим в этом обработчике изменение ресурса:
```cs
private void Button_Click(object sender, RoutedEventArgs e)
{
    this.Resources["buttonBrush"] = new SolidColorBrush(Colors.LimeGreen);
}
```

И если после запуска мы нажмем на кнопку, то ресурс изменит свой цвет, что приведет к изменению цвета кнопки. Если бы ресурс был бы определен как статический, то изменение цвета кисти никак бы не повлияло на цвет фона кнопки.

В то же время надо отметить, что мы все равно можем изменить статический ресурс — для этого нужно менять не сам объект по ключу, а его отдельные свойства:
```cs
private void Button_Click(object sender, RoutedEventArgs e)
{
    // данное изменение будет работать и со статическими ресурсами
    SolidColorBrush buttonBrush = (SolidColorBrush)this.TryFindResource("buttonBrush");
    buttonBrush.Color = Colors.LimeGreen;
}
```

#### Иерархия ресурсов
Еще одно различие между статическими и динамическими ресурсами касается поиска системой нужного ресурса. Так, при определении статических ресурсов ресурсы элемента применяются только к вложенным элементам, но не к внешним контейнерам. Например, ресурс кнопки мы не можем использовать для грида, а только для тех элементов, которые будут внутри этой кнопки. Поэтому, как правило, большинство ресурсов определяются в коллекции `Window.Resources` в качестве ресурсов всего окна, чтобы они были доступны для любого элемента данного окна.

В случае с динамическими ресурсами такого ограничения нет.

#### Установка динамических ресурсов в коде C#
Ранее мы рассмотрели, как устанавливать в коде C# статические ресурсы:
```cs
LinearGradientBrush gradientBrush = new LinearGradientBrush();
gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));
this.Resources.Add("buttonGradientBrush", gradientBrush);

button1.Background = (Brush)this.TryFindResource("buttonGradientBrush");
```

Установка динамического ресурса производится немного иначе:
```cs
LinearGradientBrush gradientBrush = new LinearGradientBrush();
gradientBrush.GradientStops.Add(new GradientStop(Colors.LightGray, 0));
gradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));
this.Resources.Add("buttonGradientBrush", gradientBrush);

button1.SetResourceReference(Button.BackgroundProperty, "buttonGradientBrush");
```

Для установки применяется метод **`SetResourceReference()`**, который есть у большинства элементов WPF. Первым параметром в него передается свойство зависимости объекта, для которого предназначен ресурс, а вторым — ключ ресурса. Общая форма установки:
```cs
объект.SetResourceReference(Класс_объекта.Свойство_КлассаProperty, ключ_ресурса);
```

<dfn title="область видимости ресурсов">Область видимости ресурсов</dfn> в WPF определяет, где и как ресурсы могут быть доступны и использованы в приложении. Ресурсы могут быть определены на разных уровнях, что влияет на их доступность.

Уровни определения ресурсов:
1. **Локальные ресурсы**: Определены внутри элемента управления, например, кнопки или панели. Эти ресурсы доступны только внутри этого элемента и его дочерних элементов.

    ```xml
    <StackPanel>
        <StackPanel.Resources>
            <sys:String x:Key="ComboBoxTitle">Items:</sys:String>
        </StackPanel.Resources>
        <Label Content="{StaticResource ComboBoxTitle}" />
    </StackPanel>
    ```

2. **Ресурсы окна**: Определены на уровне окна (`Window.Resources`). Эти ресурсы доступны всем элементам внутри этого окна

    ```xml
    <Window>
        <Window.Resources>
            <ImageBrush x:Key="TileBrush" TileMode="FlipX" ViewportUnits="Absolute"
                        Viewport="0 0 32 32" ImageSource="big_smile.png" Opacity="0.4"></ImageBrush>
        </Window.Resources>
        <Button Background="{StaticResource TileBrush}">Кнопка</Button>
    </Window>
    ```

3. **Ресурсы приложения**: Определены на уровне приложения (`Application.Resources`). Эти ресурсы доступны всем окнам и элементам внутри приложения

    ```xml
    <Application>
        <Application.Resources>
            <ImageBrush x:Key="AppTileBrush" TileMode="FlipX" ViewportUnits="Absolute"
                        Viewport="0 0 32 32" ImageSource="app_smile.png" Opacity="0.4"></ImageBrush>
        </Application.Resources>
    </Application>
    ```

WPF выполняет рекурсивный поиск ресурсов, начиная с текущего элемента и продвигаясь вверх по дереву элементов до корня приложения. Если ресурс не найден на текущем уровне, поиск продолжается на следующем более высоком уровне.

Ресурсы можно применять с помощью расширений разметки `StaticResource` и `DynamicResource`. `StaticResource` устанавливается один раз при создании окна, а `DynamicResource` может быть обновлен динамически

#### Элементы StaticResource и DynamicResource
В ряде случае в разметке XAML бывает удобнее использовать не расширения разметки тип "{StaticResource}", а полноценные элементы `DynamicResource` и `StaticResource`. Например:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"
        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <SolidColorBrush Color="LimeGreen" x:Key="buttonBrush" />
    </Window.Resources>
    <Grid>
        <Button x:Name="button1" MaxWidth="80" MaxHeight="40" Content="OK">
            <Button.Background>
                <DynamicResource ResourceKey="buttonBrush" />
            </Button.Background>
        </Button>
    </Grid>
</Window>
```

Элементы `StaticResource` и `DynamicResource` имеют свойство **`ResourceKey`**, которое позволяет установить ключ применяемого ресурса.

Особенно это эффективно может быть с контейнерами:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"
        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <Button x:Key="buttonRes" x:Shared="False" Content="OK" MaxHeight="40" MaxWidth="80" Background="Azure" />
    </Window.Resources>
    <StackPanel>
        <StaticResource ResourceKey="buttonRes" />
        <StaticResource ResourceKey="buttonRes" />
        <StaticResource ResourceKey="buttonRes" />
        <StaticResource ResourceKey="buttonRes" />
    </StackPanel>
</Window>
```

### Словари ресурсов
Мы можем определять ресурсы на уровне отдельных элементов окна, например, как ресурсы элементов `Window`, `Grid` и т.д. Однако есть еще один способ определения ресурсов, который предполагает использование словаря ресурсов.

Нажмем правой кнопкой мыши на проект и в контекстном меню выберем ***Add*** -> ***New Item...***, и в окне добавления выберем пункт **Resource Dictionary (WPF)**.

Оставим у него название по умолчанию *Dictionary1.xaml* и нажмем на кнопку OK.

После этого в проект добавляется новый файл. Он представляет собой обычный xaml-файл с одним корневым элементом `ResourceDictionary`:
```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ResourcesApp">

</ResourceDictionary>
```

Изменим его код, добавив какой-нибудь ресурс:
```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ResourcesApp">
    <LinearGradientBrush x:Key="buttonBrush">
        <GradientStopCollection>
            <GradientStop Color="White" Offset="0" />
            <GradientStop Color="Blue" Offset="1" />
        </GradientStopCollection>
    </LinearGradientBrush>
</ResourceDictionary>
```

После определения файла ресурсов его надо подсоединить к ресурсам приложения. Для этого откроем файл *App.xaml*, который есть в проекте по умолчанию и изменим его:
```xml
<Application x:Class="ResourcesApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:ResourcesApp"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Элемент `ResourceDictionary.MergedDictionaries` здесь представляет коллекцию объектов `ResourceDictionary`, то есть словарей ресурсов, которые добавляются к ресурсам приложения. Затем в любом месте приложения мы сможем сослаться на этот ресурс:
```xml
<Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{StaticResource buttonBrush}" />
```

При этом одновременно мы можем добавлять в коллекцию ресурсов приложения множество других словарей или параллельно с ними определять еще какие-либо ресурсы:
```xml
<Application x:Class="ResourcesApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:ResourcesApp"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" />
                <ResourceDictionary Source="Dictionary2.xaml" />
                <ResourceDictionary Source="ButtonStyles.xaml" />
                <SolidColorBrush Color="LimeGreen" x:Key="limeButton" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

#### Загрузка словаря ресурсов
Нам необязательно добавлять словарь ресурсов через ресурсы приложения. У объекта `ResourceDictionary` имеется свойство `Source`, через которое мы можем связать ресурсы конкретного элемента со словарем:
```xml
<Window x:Class="ResourcesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ResourcesApp"
        mc:Ignorable="d"
        Title="Ресурсы" Height="250" Width="300">
    <Window.Resources>
        <ResourceDictionary Source="Dictionary1.xaml" />
    </Window.Resources>
    <Grid>
        <Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{StaticResource buttonBrush}" />
    </Grid>
</Window>
```

Также мы можем загружать словарь динамически в коде C#. Так, загрузим в коде C# вышеопределенный словарь:
```cs
this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary1.xaml") };
```

При динамической загрузке, если мы определяем ресурсы через *xaml*, то они должны быть динамическими:
```xml
<Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{DynamicResource buttonBrush}" />
```

