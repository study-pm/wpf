## UI Elements / Элементы интерфейса

- [UI Elements / Элементы интерфейса](#ui-elements--элементы-интерфейса)
  - [Visual / Визуальные элементы](#visual--визуальные-элементы)
  - [UIElement / Элемент пользовательского интерфейса](#uielement--элемент-пользовательского-интерфейса)
  - [FrameworkElement / Элементы фреймворка](#frameworkelement--элементы-фреймворка)
  - [TextBlock / Текстовый блок](#textblock--текстовый-блок)
    - [Отображение многострочного содержимого](#отображение-многострочного-содержимого)
    - [TextBlock и Label](#textblock-и-label)
    - [Inline / Встроенные элементы](#inline--встроенные-элементы)
      - [Bold, Italic и Underline](#bold-italic-и-underline)
      - [LineBreak](#linebreak)
      - [Hyperlink](#hyperlink)
      - [Run](#run)
      - [Span](#span)
      - [InlineUIContainer](#inlineuicontainer)
      - [AnchoredBlock](#anchoredblock)
    - [Программное форматирование](#программное-форматирование)
  - [Popup / Всплывающее окно](#popup--всплывающее-окно)
    - [Popup и ToolTip](#popup-и-tooltip)
    - [Скрытие окна](#скрытие-окна)
  - [Image / Изображение](#image--изображение)
  - [InkCanvas / Полотно](#inkcanvas--полотно)
    - [Режимы рисования](#режимы-рисования)
    - [Настройка внешнего вида](#настройка-внешнего-вида)
    - [Сохранение и загрузка](#сохранение-и-загрузка)

Иерархия базовых классов, являющихся основой элементов интерфейса, обеспечивает постепенное добавление функциональности:

- **`Visual`**: Отвечает за отрисовку и визуализацию объектов.

- **`UIElement`**: Добавляет возможности по компоновке, обработку событий и получение ввода.

- **`FrameworkElement`**: Расширяет возможности `UIElement`, добавляя поддержку привязки данных, анимации, стилизации и другие свойства, связанные с компоновкой.

### Visual / Визуальные элементы
`Visual` в WPF — это базовый абстрактный класс, который обеспечивает фундаментальные возможности для отрисовки и визуализации объектов. Он является основой для всех элементов управления в WPF и служит точкой входа для создания пользовательских элементов интерфейса.

Определение:
```cs
public abstract class Visual : System.Windows.DependencyObject
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual?view=windowsdesktop-9.0

Основные особенности:
- **Отрисовка и визуализация**: Класс `Visual` инкапсулирует инструкции рисования и обеспечивает базовую функциональность для отображения объектов на экране. Он поддерживает отсечение, прозрачность и настройки трансформации.

- **Проверка попадания**: Visual позволяет определять, содержится ли указанная координата или геометрия в границах визуального элемента.

- **Вычисление ограничивающего прямоугольника**: Класс может вычислять ограничивающий прямоугольник визуального объекта, что важно для компоновки и отрисовки.

- **Связь с `milcore.dll`**: `Visual` обеспечивает связь между управляемыми библиотеками WPF и сборкой `milcore.dll`, которая визуализирует отображение.

`Visual` наследуется от `DependencyObject` и является базовым классом для `UIElement`, который добавляет поддержку компоновки, ввода и событий.

Использование `Visual` и его наследников, таких как `DrawingVisual`, позволяет создавать легковесные и эффективные графические интерфейсы. Это особенно полезно при разработке пользовательских элементов управления или при необходимости прямого доступа к низкоуровневым графическим возможностям WPF.

Пример использования `Visual` обычно включает создание пользовательских элементов интерфейса, наследующих от `Visual` или его производных классов. Например, `DrawingVisual` может быть использован для отрисовки пользовательских графиков или фигур:
```cs
using System.Windows;
using System.Windows.Media;

public class MyVisual : Visual
{
    protected override void OnRender(DrawingContext drawingContext)
    {
        // Код для отрисовки элемента
        drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, 100, 100));
    }
}
```

В этом примере `MyVisual` наследует от `Visual` и переопределяет метод `OnRender`, чтобы нарисовать красный прямоугольник.

### UIElement / Элемент пользовательского интерфейса
`UIElement` в WPF — это базовый класс, который предоставляет фундаментальные возможности для элементов управления, таких как поддержка компоновки, обработка событий и получение ввода. Он является ключевым элементом в иерархии классов WPF, расширяя возможности `Visual` и добавляя функциональность, необходимую для работы с элементами управления на уровне ядра WPF.

Определение:
```cs
[System.Windows.Markup.UidProperty("Uid")]
public class UIElement : System.Windows.Media.Visual, System.Windows.IInputElement, System.Windows.Media.Animation.IAnimatable
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement?view=windowsdesktop-9.0

Основные особенности:
- **Компоновка и отрисовка**: `UIElement` наследуется от **`Visual`**, что обеспечивает базовые возможности для отрисовки и визуализации объектов. Он также предоставляет фундаментальные методы для компоновки элементов, такие как определение размеров и позиционирования.

- **Обработка событий и ввода**: Класс `UIElement` добавляет поддержку событий ввода, включая клавиатуру, мышь и другие устройства ввода. Это позволяет элементам реагировать на взаимодействие с пользователем, например, на нажатие кнопок мыши или клавиш клавиатуры.

- **Маршрутизация событий**: `UIElement` поддерживает маршрутизацию событий, что позволяет событиям распространяться по дереву элементов. Это включает как восходящую (туннелированную), так и нисходящую (пузырьковую) маршрутизацию событий.

- **Анимация**: Класс `UIElement` реализует интерфейс `IAnimatable`, что позволяет использовать анимацию для элементов, производных от него.

Пример создания пользовательского элемента управления, наследующего от `UIElement`:
```cs
public class MyUserControl : UIElement
{
    protected override void OnRender(DrawingContext drawingContext)
    {
        // Код для отрисовки элемента
        drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, 100, 100));
    }
}
```

В этом примере `MyUserControl` наследует от `UIElement` и переопределяет метод `OnRender`, чтобы нарисовать красный прямоугольник.

Важные аспекты:
- **`Visibility`**: Состояние видимости элемента влияет на обработку событий ввода. Элементы, которые не видны, не получают события ввода.

- **События**: `UIElement` предоставляет широкий спектр событий для обработки различных типов ввода и взаимодействия с пользователем.

Таким образом, `UIElement` наследуется от `Visual` и реализует интерфейсы `IInputElement` и `IAnimatable`. Реализация интерфейса `IInputElement` в `UIElement` обеспечивает базовые возможности для обработки ввода в WPF, а интерфейса `IAnimatable` — поддержку анимации для элементов управления в WPF. Он является базовым классом для `FrameworkElement`, который добавляет поддержку привязки данных, стилизации и других свойств, связанных с компоновкой.

### FrameworkElement / Элементы фреймворка
`FrameworkElement` в WPF — это базовый класс, который предоставляет фундаментальные возможности для элементов управления, таких как поддержка привязки данных, анимации, стилизацию и компоновку. Он является ключевым элементом в иерархии классов WPF, расширяя возможности `UIElement` и добавляя функциональность, необходимую для работы с элементами управления на уровне платформы WPF.

Определение:
```cs
[System.Windows.Markup.RuntimeNameProperty("Name")]
[System.Windows.Markup.UsableDuringInitialization(true)]
[System.Windows.Markup.XmlLangProperty("Language")]
[System.Windows.StyleTypedProperty(Property="FocusVisualStyle", StyleTargetType=typeof(System.Windows.Controls.Control))]
public class FrameworkElement : System.Windows.UIElement, System.ComponentModel.ISupportInitialize, System.Windows.IFrameworkInputElement, System.Windows.Markup.IQueryAmbient
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-9.0

Основные особенности:
- **Поддержка привязки данных и ресурсов**: `FrameworkElement` позволяет использовать привязку данных и динамические ссылки на ресурсы, что упрощает связывание элементов управления с данными и ресурсами приложения.

- **Свойства компоновки**: Класс предоставляет свойства, связанные с компоновкой, такие как выравнивание и отступы, что позволяет точно позиционировать элементы на экране.

- **Анимация и стилизация**: `FrameworkElement` поддерживает анимацию и стилизацию, что позволяет создавать динамические и визуально привлекательные интерфейсы.

- **События времени существования объекта**: Класс определяет события, связанные с инициализацией и загрузкой элементов, что позволяет программно обрабатывать различные этапы жизненного цикла элементов.

`FrameworkElement` позволяет определять свойства зависимостей (dependency properties), которые являются ключевым механизмом в WPF для реализации привязки данных, анимации и других функций. Эти свойства регистрируются с помощью метода `DependencyProperty.Register` и используются для хранения и обновления значений свойств в элементах управления.

Пример создания пользовательского элемента управления, наследующего от `FrameworkElement`:
```cs
public class MyUserControl : FrameworkElement
{
    // Определение свойства зависимости
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("MyProperty", typeof(string), typeof(MyUserControl));

    public string MyProperty
    {
        get { return (string)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }
}
```

В этом примере `MyUserControl` наследует от `FrameworkElement` и определяет свойство зависимости `MyProperty`, которое может быть использовано для привязки данных или других целей в WPF.

`FrameworkElement` реализует три интерфейса:

- **`System.ComponentModel.ISupportInitialize`**

    Этот интерфейс предоставляет два метода: `BeginInit()` и `EndInit()`. Они используются для управления процессом инициализации элементов. Эти методы автоматически вызываются анализатором разметки XAML, но если элементы создаются в коде, их следует вызывать вручную. Реализация `ISupportInitialize` позволяет обеспечить правильную последовательность инициализации свойств элементов, особенно при создании сложных объектов с взаимосвязанными свойствами.

- **`System.Windows.IFrameworkInputElement`**

    Этот интерфейс обеспечивает поддержку ввода для элементов управления в WPF. Он позволяет элементам получать и обрабатывать события ввода, такие как нажатия клавиш клавиатуры и клики мыши. Реализация `IFrameworkInputElement` является важной для обеспечения интерактивности элементов в приложениях WPF.

- **`System.Windows.Markup.IQueryAmbient`**

    Этот интерфейс используется для запроса окружающих свойств в дереве элементов WPF. <dfn title="окружающие свойства">Окружающие свойства</dfn> — это свойства, которые наследуются от родительских элементов и могут быть использованы дочерними элементами. Реализация `IQueryAmbient` позволяет элементам получать доступ к этим свойствам и использовать их для настройки своего поведения или внешнего вида.

В целом, реализация этих интерфейсов в `FrameworkElement` обеспечивает базовые возможности для инициализации, обработки ввода и доступа к окружающим свойствам, что делает его универсальным и гибким классом для построения элементов управления в WPF.

### TextBlock / Текстовый блок
[671f479b5040133e8429e58a](https://metanit.com/sharp/wpf/5.5.php)

`TextBlock` в WPF — это элемент, предназначенный для отображения текста в приложениях. Он является легковесным и простым в использовании (благодаря тому, что наследуется напрямую от `FrameworkElement`, а не класса `Control`), что делает его идеальным для вывода небольших порций текста в пользовательском интерфейсе.

Определение:
```cs
[System.Windows.Localizability(System.Windows.LocalizationCategory.Text)]
[System.Windows.Markup.ContentProperty("Inlines")]
public class TextBlock : System.Windows.FrameworkElement, IServiceProvider, System.Windows.IContentHost
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.textblock?view=windowsdesktop-9.0

Элемент предназначен для вывода текстовой информации, для создания простых надписей:
```xml
<StackPanel>
    <TextBlock>Текст1</TextBlock>
    <TextBlock Text="Текст2" />
</StackPanel>
```

Ключевым свойством здесь является свойство `Text`, которое задает текстовое содержимое. Причем в случае `<TextBlock>Текст1</TextBlock>` данное свойство задается неявно.

```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="100" Width="200">
    <Grid>
		<TextBlock>This is a TextBlock</TextBlock>
    </Grid>
</Window>
```

Для изменения параметров отображаемого текста данный элемент имеет такие свойства, как `LineHeight`, `TextWrapping` и `TextAlignment`.

- Свойство **`LineHeight`** позволяет указывать высоту строк.

- Свойство **`TextWrapping`** позволяет переносить текст при установке этого свойства `TextWrapping="Wrap"`. По умолчанию это свойство имеет значение `NoWrap`, поэтому текст не переносится.

- Свойство **`TextAlignment`** выравнивает текст по центру (значение `Center`), правому (`Right`) или левому краю (`Left`): `<TextBlock TextAlignment="Right">`.

- Для декорации текста используется свойство **`TextDecorations`**, например, если `TextDecorations="Underline"`, то текст будет подчеркнут.

С помощью таких свойств, как `FontFamily`, `TextDecorations` и др., мы можем настроить отображение текста. Однако мы можем задать и более сложное форматирование, например:
```xml
<TextBlock TextWrapping="Wrap">
    <Run FontSize="20" Foreground="Red" FontWeight="Bold">О</Run>
    <Run FontSize="16" Foreground="LightSeaGreen">негин был, по мненью многих...</Run>
</TextBlock>
```

Основные особенности:
- **Отображение текста**: `TextBlock` может содержать текст, который задается с помощью свойства `Text`. Текст может быть добавлен как в XAML, так и в коде.

- **Форматирование текста**: `TextBlock` поддерживает различные свойства для форматирования текста, такие как `FontFamily`, `FontSize`, `FontWeight`, `FontStyle`, и `TextDecorations`. Эти свойства позволяют изменять шрифт, размер, жирность и стиль текста, а также добавлять подчеркивания или зачеркивания.

- **Перенос строк**: Для того чтобы текст переносился на новую строку, можно использовать свойство `TextWrapping` с значением `Wrap`. По умолчанию текст не переносится.

- **Выравнивание текста**: Свойство `TextAlignment` позволяет выравнивать текст по левому, правому краю или центру.

- **Встроенное содержимое**: `TextBlock` поддерживает встроенное содержимое, такое как `Run`, `Bold`, `Italic`, и `Hyperlink`, что позволяет создавать сложные форматы текста внутри одного элемента.

Пример создания простого `TextBlock` с помощью XAML:
```xml
<TextBlock Text="Привет, мир!"
           FontSize="18"
           FontWeight="Bold"
           FontStyle="Italic" />
```

Или в коде:
```cs
TextBlock myTextBlock = new TextBlock();
myTextBlock.Text = "Привет, мир!";
myTextBlock.FontSize = 18;
myTextBlock.FontWeight = FontWeights.Bold;
myTextBlock.FontStyle = FontStyles.Italic;
```

Преимущества использования:
- **Легковесность**: `TextBlock` не наследует класс `Control`, что делает его более легким и менее требовательным к ресурсам по сравнению с другими элементами управления, такими как `Label`.

- **Гибкость**: Он может быть использован для отображения как простого текста, так и сложных форматов с помощью встроенных элементов, таких как `Run` и `Bold`.

Для обеспечения определенных функциональных возможностей `TextBlock` в WPF реализует интерфейсы `IServiceProvider` и `IContentHost`:

- **`IServiceProvider`**

    Этот интерфейс позволяет `TextBlock` предоставлять службы другим элементам или компонентам в приложении. В контексте WPF, `IServiceProvider` часто используется для доступа к службам, предоставляемым родительскими элементами или контекстом приложения. Реализация этого интерфейса в `TextBlock` позволяет ему участвовать в общей инфраструктуре WPF для предоставления и получения служб.

- **`IContentHost`**

    Этот интерфейс обеспечивает поддержку для хранения и управления содержимым внутри `TextBlock`. `IContentHost` позволяет `TextBlock` содержать встроенные элементы, такие как `Run`, `Bold`, `Italic`, и другие, что делает его гибким для форматирования и структурирования текста. Свойство `Inlines` в `TextBlock` является ключевым для реализации этого интерфейса, поскольку оно позволяет добавлять и управлять встроенными элементами.

Реализация этих интерфейсов в `TextBlock` дает следующие преимущества:

- **Гибкость содержимого**: `IContentHost` позволяет создавать сложные форматы текста с помощью встроенных элементов.

- **Интеграция с другими элементами**: `IServiceProvider` обеспечивает интеграцию с другими элементами и службами в приложении WPF.

В целом, реализация этих интерфейсов делает `TextBlock` более универсальным и интегрированным элементом в экосистеме WPF.

#### Отображение многострочного содержимого
Элемент `TextBlock` идеально подходит для коротких текстовых фрагментов, однако он в равной степени хорошо справляется и с длинным, а также многострочным текстом. Однако по умолчанию элемент отображает лишь ту часть текста, которая может быть помещена внутри него в одну строку, обрезая то, что выходит за пределы границ элемента.

```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="100" Width="200">
    <Grid>
		<TextBlock Margin="10">This is a TextBlock control and it comes with a very long text</TextBlock>
    </Grid>
</Window>
```

Существует несколько способов повлиять на данную ситуацию:
```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="200" Width="250">
    <StackPanel>
		<TextBlock Margin="10" Foreground="Red">
			This is a TextBlock control<LineBreak />
			with multiple lines of text.
		</TextBlock>
		<TextBlock Margin="10" TextTrimming="CharacterEllipsis" Foreground="Green">
			This is a TextBlock control with text that may not be rendered completely, which will be indicated with an ellipsis.
		</TextBlock>
		<TextBlock Margin="10" TextWrapping="Wrap" Foreground="Blue">
			This is a TextBlock control with automatically wrapped text, using the TextWrapping property.
		</TextBlock>
	</StackPanel>
</Window>
```

В примере выше показаны три элемента `TextBlock`, для удобства показанные разными цветами (заданные свойством `Foreground`), каждый из которых использует разные подходы к обработке длинного текстового содержимого.

- <span style="color: red">Красный TextBlock</span> задействует тег **`LineBreak`** для ручного переноса строки (возврата каретки) в установленном месте. Это дает абсолютный контроль над тем, где именно должен быть переход на новую строку, но в большинстве ситуаций данный подход недостаточно гибкий. При увеличении размера окна текст останется прерванным в том же самом месте, хотя пространства для размещения текста в одну строку уже будет достаточно.

- <span style="color: green">Зеленый TextBlock</span> использует свойство **`TextTrimming`** со значением **`CharacterEllipsis`** для того, чтобы отобразить многоточие (`...`) в том месте, где текст перестает помещаться в элемент. Это распространённый метод сигнализирования о том, что текста имеется больше, чем позволяет вместить контейнер. Это прекрасный вариант в случае длинного текста, который необходимо отобразить в формате одной строки. Как вариант, вместо **`CharacterEllipsis`** можно использовать значение **`WordEllipsis`**, которое при недостатке пространства обрезает текст не по последнему видимому символу, а до целого слова, что позволяет избежать ситуации с постановкой многоточия посреди слова.

- <span style="color: blue">Синий TextBlock</span> использует свойство **`TextWrapping`** со значением **`Wrap`** для переноса на следующую строка текста, который не умещается в предыдущей строке. В противоположность первому элементу `TextBlock`, где мы вручную указывали место переноса, на этот раз процесс является полностью автоматическим и имеет еще одно достоинство: при изменении размера контейнера и, соответственно, доступного пространства, места переносов обновляются сами. Делая окно больше или меньше, можно заметить, как переносы изменяются в соответствии с новыми условиям.

#### TextBlock и Label
`TextBlock` и `Label` могут использоваться для отображения текста, но `Label` поддерживает доступные клавиши (Access Key) и может содержать не только текст, но и другие элементы, такие как изображения. В свою очередь, `TextBlock` позволяет разместить текст на экране, как `Label`, но при этом он проще в использовании и менее требователен к ресурсам, однако `TextBlock` предназначен исключительно для текста и не поддерживает доступные клавиши.

В целом, элемент `Label` ассоциируется с коротким однострочным текстом (который может включать в себя, например, изображение), в то время как `TextBlock` отлично справляется еще и с многострочным текстом, но может содержать в себе исключительно строковые данные. Таким образом, выбор того или иного элемента зависит от конкретной ситуации с учётом достоинств и недостатков каждого из них.

#### Inline / Встроенные элементы
Встроенные элементы в `TextBlock` — это специальные элементы, которые наследуют класс `Inline` и позволяют создавать сложные форматы текста внутри одного `TextBlock`. Эти элементы могут быть использованы для изменения стиля, цвета или других свойств текста в различных частях `TextBlock`.

Основные встроенные элементы:
- **`Bold`**: Отображает текст в жирном стиле.

- **`Italic`**: Отображает текст в курсивном стиле.

- **`Underline`**: Добавляет подчеркивание к тексту.

- **`LineBreak`**: Создает перенос строки в определенном месте.

- **`Hyperlink`**: Создает ссылку, которая может быть кликнута для открытия URL или выполнения другого действия.

- **`Run`**: Базовый элемент для отображения текста. Позволяет задавать свойства, такие как `FontSize`, `FontWeight`, и `Foreground`.

- **`Span`**: Группирует несколько элементов `Run` или других встроенных элементов для применения общих стилей.

- **`InlineUIContainer`**: Позволяет встраивать другие элементы управления WPF, такие как кнопки или изображения, в текст.

- **`AnchoredBlock`**: Позволяет создавать элементы, которые могут быть привязаны к определенному месту внутри потоковых документов.

Пример создания `TextBlock` с встроенными элементами:
```xml
<TextBlock TextWrapping="Wrap">
    <Bold>Жирный текст</Bold>
    <Run Text=" обычый текст" />
    <Italic>курсивный текст</Italic>
    <Underline>подчеркнутый текст</Underline>
    <Hyperlink NavigateUri="https://example.com">Ссылка</Hyperlink>
</TextBlock>
```

Или в коде:
```cs
TextBlock textBlock = new TextBlock();
textBlock.TextWrapping = TextWrapping.Wrap;

textBlock.Inlines.Add(new Bold(new Run("Жирный текст")));
textBlock.Inlines.Add(new Run(" обычного текста"));
textBlock.Inlines.Add(new Italic(new Run("курсивный текст")));
textBlock.Inlines.Add(new Underline(new Run("подчеркнутый текст")));
textBlock.Inlines.Add(new Hyperlink(new Run("Ссылка")) { NavigateUri = new Uri("https://example.com") });
```

Преимущества использования:
- **Гибкость форматирования**: Встроенные элементы позволяют создавать сложные форматы текста внутри одного `TextBlock`.

- **Легкость использования**: Эти элементы легко добавляются как в XAML, так и в коде.

- **Универсальность**: Поддержка различных стилей и элементов, таких как ссылки и контейнеры для других элементов управления.

##### Bold, Italic и Underline
Пожалуй, наиболее простыми из всех встроенных элементов являются те, что позволяют задавать начертание шрифта:
```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockInlineSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockInlineSample" Height="100" Width="300">
    <Grid>
		<TextBlock Margin="10" TextWrapping="Wrap">
			TextBlock with <Bold>bold</Bold>, <Italic>italic</Italic> and <Underline>underlined</Underline> text.
		</TextBlock>
    </Grid>
</Window>
```

Почти как в HTML необходимый фрагмент оборачивается в тег `Bold`, чтобы получить жирный текст. Это сильно упрощает процесс создания и отображения форматированного текста в приложении.

Все три рассматриваемых тега являются дочерними классами `Span`, каждый из которых устанавливает определенное свойство элементу `Span` для получения желаемого эффекта. Например, тег `Bold` просто определяет свойство `FontWeight` базового элемента `Span`, `Italic` — `FontStyle` и т.д.

##### LineBreak
Если нам вдруг потребуется перенести текст на другую строку, то тогда мы можем использовать элемент `LineBreak`:
```xml
<TextBlock>
    Однажды в студеную зимнюю пору
    <LineBreak />
    Я из лесу вышел
</TextBlock>
```

##### Hyperlink
Элемент `Hyperlink` позволяет вставлять ссылки в текст. Они отображаются в соответствии с текущей темой Windows, обычно в формате подчеркнутого синего текста, который при наведении указателя мыши становится красным, отображая курсор в виде руки. Для определения целевого URL можно использовать свойство `NavigateUri`. Вот пример:
```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockHyperlinkSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockHyperlinkSample" Height="100" Width="300">
	<Grid>
		<TextBlock Margin="10" TextWrapping="Wrap">
			This text has a <Hyperlink RequestNavigate="Hyperlink_RequestNavigate" NavigateUri="https://www.google.com">link</Hyperlink> in it.
		</TextBlock>
	</Grid>
</Window>
```

Элемент `Hyperlink` также используются внутри элемента WPF `Page` для навигации между страницами. В таком случае нет необходимости специально обрабатывать событие `RequestNavigate`, как в примере выше, но для  соединения со внешними URL из обыкновенного приложения WPF, это событие придется использовать, причем вместе с классом `Process`. Для этого необходимо подписаться на событие `RequestNavigate`, которое позволит запустить содержимое по внешнему URL в пользовательском браузере, установленном в системе по умолчанию, при помощи простого обработчика событий, как, например, в отделенном коде, представленном ниже:
```cs
private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
{
	System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
}
```

##### Run
Элементы `Run` представляют куски обычного текста, для которых можно задать отдельное форматирование. Элемент `Run` позволяет форматировать сроку, используя все свойства, доступные для элемента `Span`, но в отличие от `Span`, поддерживающего другие встроенные элементы, `Run` может содержать лишь простой текст. Это делает `Span` более гибким элементом, и, следовательно, логичным выбором в большинстве случаев.

##### Span
Элемент `Span` не имеет специального отображения по умолчанию, но позволяет устанавливать практически любой формат отображения, включая размер шрифта, стиль, начертание, цвет фона, цвет текста и другие. Самым ценным в `Span` является то, что он поддерживает другие встроенные элементы внутри себя, позволяя без особых проблем определять самые замысловатые комбинации стилизованного текста. В следующем примере используется множество элементов `Span` для демонстрации широких возможностей, которые дает их применение:
```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSpanSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSpanSample" Height="100" Width="300">
    <Grid>
		<TextBlock Margin="10" TextWrapping="Wrap">
			This <Span FontWeight="Bold">is</Span> a
			<Span Background="Silver" Foreground="Maroon">TextBlock</Span>
			with <Span TextDecorations="Underline">several</Span>
			<Span FontStyle="Italic">Span</Span> elements,
			<Span Foreground="Blue">
				using a <Bold>variety</Bold> of <Italic>styles</Italic>
			</Span>.
		</TextBlock>
	</Grid>
</Window>
```

Таким образом видно, что элемент `Span` является идеальным вариантом в случае, если ни один из элементов не подходит или просто необходимо определить пустой контейнер для последующего форматировании текста.

##### InlineUIContainer
`InlineUIContainer` — это класс в WPF, который позволяет встраивать элементы управления (`UIElement`) в потоковые документы на уровне строковых элементов. Это означает, что вы можете включать кнопки, изображения или другие элементы управления непосредственно в текст внутри элементов, таких как `Paragraph`.

Основные свойства и использование:
- **`Child`**: Это свойство определяет элемент управления (`UIElement`), который будет встроен в потоковое содержимое.

- **`InlineUIContainer`** может содержать только один верхний элемент управления, но этот элемент может содержать другие элементы управления внутри себя.

Пример использования `InlineUIContainer` в потоковом документе (`FlowDocument`):
```xml
<FlowDocument>
    <Paragraph>
        <Run>Текст перед кнопкой.</Run>
        <InlineUIContainer>
            <Button>Нажмите меня!</Button>
        </InlineUIContainer>
        <Run>Текст после кнопки.</Run>
    </Paragraph>
</FlowDocument>
```

Пример программного создания `InlineUIContainer`:
```cs
FlowDocument document = new FlowDocument();
Paragraph paragraph = new Paragraph();

Run run1 = new Run("Текст перед кнопкой.");
paragraph.Inlines.Add(run1);

InlineUIContainer container = new InlineUIContainer();
Button button = new Button { Content = "Нажмите меня!" };
container.Child = button;
paragraph.Inlines.Add(container);

Run run2 = new Run("Текст после кнопки.");
paragraph.Inlines.Add(run2);

document.Blocks.Add(paragraph);
```

Важные Моменты:
- `InlineUIContainer` должен быть размещен внутри элемента, который поддерживает строковые элементы, например, внутри `Paragraph`.

- Если вы используете `InlineUIContainer` в контексте `RadRichTextBox`, необходимо задать явно размеры (`Width` и `Height`) для отображения элемента.

`InlineUIContainer` является строковым элементом, в то время как `BlockUIContainer` — блочным. Это означает, что `InlineUIContainer` будет отображаться на одной строке с текстом, а `BlockUIContainer` будет занимать всю ширину и создавать новый блок содержимого

`InlineUIContainer` не может быть напрямую использован внутри `TextBlock`, поскольку он предназначен для использования в потоковых документах (`FlowDocument`).

##### AnchoredBlock
`AnchoredBlock` — абстрактный класс в пространстве имен `System.Windows.Documents`, который служит базовым классом для элементов `Inline`, используемых для привязки элементов `Block` к потоковому содержимому. Этот класс позволяет создавать элементы, которые могут быть привязаны к определенному месту внутри потоковых документов, таких как `FlowDocument`.

Основные Свойства и Использование:
- **`Parent`**: Ссылка на родительский элемент, который содержит `AnchoredBlock`.

- **`AnchorBlock`**: Ссылка на элемент Block, который привязан к потоковому содержимому.

Существует два основных типа классов, наследующих от `AnchoredBlock`:

1. **`Figure`**: Используется для создания фигур, которые могут быть привязаны к потоковому содержимому. Фигуры могут содержать изображения, текст или другие элементы.

2. **`Floater`**: Используется для создания плавающих элементов, которые могут быть привязаны к потоковому содержимому.

Пример использования `Figure` внутри `FlowDocument`:
```xml
<FlowDocument>
    <Paragraph>
        <Figure HorizontalAnchor="ContentLeft" Width="100" Height="100">
            <BlockUIContainer>
                <Image Source="myPhoto.jpg" />
            </BlockUIContainer>
        </Figure>
        Этот текст будет отображаться рядом с изображением.
    </Paragraph>
</FlowDocument>
```

Пример программного создания `Figure`:
```cs
Figure figure = new Figure();
figure.HorizontalAnchor = FigureHorizontalAnchor.ContentLeft;
figure.Width = new FigureLength(100);
figure.Height = new FigureLength(100);

BlockUIContainer container = new BlockUIContainer();
Image image = new Image();
image.Source = new BitmapImage(new Uri("myPhoto.jpg"));
container.Child = image;

figure.Blocks.Add(new BlockUIContainer { Child = image });

FlowDocument document = new FlowDocument();
Paragraph paragraph = new Paragraph();
paragraph.Inlines.Add(figure);
document.Blocks.Add(paragraph);
```

Важные Моменты:
- `AnchoredBlock` используется в основном в потоковых документах (FlowDocument) для привязки элементов к определенному месту внутри текста.

- Классы, наследующие от `AnchoredBlock`, такие как `Figure` и `Floater`, позволяют создавать привязанные элементы с различными свойствами позиционирования.

`AnchoredBlock` не может быть напрямую использован как `Inline` элемент внутри `TextBlock`, поскольку он предназначен для использования в потоковых документах (`FlowDocument`).

#### Программное форматирование
Как можно заметить, форматирование текста с помощью XAML не представляет особых трудностей, однако в определенных случаях необходимо этот процесс выполнять на C# из файла отделённого года. Подобная реализация, как правило, получается немного более громоздкой, однако ниже представлен пример, как это можно сделать:
```xml
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockCodeBehindSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockCodeBehindSample" Height="100" Width="300">
    <Grid></Grid>
</Window>
```

```cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfTutorialSamples.Basic_controls
{
	public partial class TextBlockCodeBehindSample : Window
	{
		public TextBlockCodeBehindSample()
		{
			InitializeComponent();
			TextBlock tb = new TextBlock();
			tb.TextWrapping = TextWrapping.Wrap;
			tb.Margin = new Thickness(10);
			tb.Inlines.Add("An example on ");
			tb.Inlines.Add(new Run("the TextBlock control ") { FontWeight = FontWeights.Bold });
			tb.Inlines.Add("using ");
			tb.Inlines.Add(new Run("inline ") { FontStyle = FontStyles.Italic });
			tb.Inlines.Add(new Run("text formatting ") { Foreground = Brushes.Blue });
			tb.Inlines.Add("from ");
			tb.Inlines.Add(new Run("Code-Behind") { TextDecorations = TextDecorations.Underline });
			tb.Inlines.Add(".");
			this.Content = tb;
		}
	}
}
```

Здорово иметь возможность влиять на форматирование программно, а в определенных случаях это просто необходимо, тем не менее, пример выше, скорее всего, заставит вас еще больше ценить XAML.

### Popup / Всплывающее окно
`Popup` в WPF — это элемент управления, который отображает содержимое в отдельном окне, расположенном поверх текущего окна приложения. Он позволяет создавать всплывающие окна, которые могут содержать любые элементы WPF, такие как кнопки, текстовые блоки или даже сложные пользовательские элементы управления.

Определение:
```cs
[System.Windows.Localizability(System.Windows.LocalizationCategory.None)]
[System.Windows.Markup.ContentProperty("Child")]
public class Popup : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.primitives.popup?view=windowsdesktop-9.0

Основные особенности:
- **Отображение содержимого**: `Popup` может содержать любой элемент WPF, который хранится в свойстве `Child`. Это позволяет создавать всплывающие окна с различным содержимым, от простого текста до сложных интерфейсов.

- **Положение и размещение**: Положение `Popup` можно задать с помощью свойств `PlacementTarget`, `PlacementRectangle`, `Placement`, `HorizontalOffset`, и `VerticalOffset`. Это позволяет точно контролировать, где будет отображаться всплывающее окно.

- **Открытие и закрытие**: `Popup` не открывается автоматически; для его отображения необходимо установить свойство `IsOpen` в `true`. Закрытие происходит при установке `IsOpen` в `false` или при настройке поведения закрытия с помощью свойства `StaysOpen`.

- **Анимация и прозрачность**: `Popup` поддерживает анимацию при открытии и закрытии, если свойству `AllowsTransparency` присвоено значение `true`. Анимация может быть настроена с помощью свойства `PopupAnimation`.

Пример создания простого `Popup` с помощью XAML:
```xml
<Popup Name="myPopup">
    <TextBlock Name="myPopupText"
               Background="LightBlue"
               Foreground="Blue">
        Popup Text
    </TextBlock>
</Popup>
```

Или в коде:
```cs
Popup codePopup = new Popup();
TextBlock popupText = new TextBlock();
popupText.Text = "Popup Text";
popupText.Background = Brushes.LightBlue;
popupText.Foreground = Brushes.Blue;
codePopup.Child = popupText;
```

Основные свойства:

1. **`Child`**: Определяет содержимое элемента `Popup`. Может быть любым элементом `UIElement`.

    ```xml
    <Popup><TextBlock>Popup Text</TextBlock></Popup>
    ```

2. **`IsOpen`**: Управляет видимостью `Popup`. Если установлено в `true`, `Popup` отображается.

    ```xml
    <Popup IsOpen="True">...</Popup>
    ```

3. **`PlacementTarget`**: Определяет элемент, относительно которого будет позиционирован `Popup`.

    ```xml
    <Popup PlacementTarget="{Binding ElementName=myButton}">...</Popup>
    ```

4. **`Placement`**: Определяет положение `Popup` относительно `PlacementTarget`. Возможные значения: `Absolute`, `Bottom`, `Center`, `Left`, `Mouse`, `MousePoint`, `Relative`, `RelativePoint`, `Right`, `Top`.

    ```xml
    <Popup Placement="Bottom">...</Popup>
    ```

5. **`HorizontalOffset`** и **`VerticalOffset`**: Позволяют сместить `Popup` от заданного положения.

    ```xml
    <Popup HorizontalOffset="10" VerticalOffset="20">...</Popup>
    ```

6. **`AllowsTransparency`**: Если установлено в true, позволяет использовать прозрачное содержимое в Popup. Также необходимо для анимаций.

    ```xml
    <Popup AllowsTransparency="True">...</Popup>
    ```

7. **`PopupAnimation`**: Определяет анимацию, которая будет использоваться при открытии `Popup`. Возможные значения: `None`, `Fade`, `Scroll`, `Slide`.

    ```xml
    <Popup PopupAnimation="Fade">...</Popup>
    ```

8. **`StaysOpen`**: Если установлено в `false`, Popup закрывается при клике вне его области.

    ```xml
    <Popup StaysOpen="False">...</Popup>
    ```

9. **`CustomPopupPlacementCallback`**: Позволяет задать пользовательскую логику позиционирования `Popup`

    ```xml
    <Popup CustomPopupPlacementCallback="MyCustomCallback">...</Popup>
    ```

Преимущества использования:
- **Гибкость**: `Popup` может содержать любые элементы WPF, что делает его универсальным инструментом для создания всплывающих окон.

- **Интерактивность**: В отличие от `ToolTip`, `Popup` может получать фокус и содержать интерактивные элементы, такие как кнопки.

- **Настройка внешнего вида**: Внешний вид `Popup` можно настроить с помощью свойств `Background` и `Border`, что позволяет создавать всплывающие окна в соответствии с дизайном приложения.

- **Поведение открытия и закрытия**: `Popup` остается открытым до тех пор, пока свойство `IsOpen` не будет установлено в `false`. Если необходимо закрыть `Popup` при клике вне его границ, можно использовать события мыши для обработки этого поведения

#### Popup и ToolTip
Как и `ToolTip`, элемент `Popup` также представляет всплывающее окно, только в данном случае оно имеет другую функциональность. Если `Tooltip` отображается автоматически при наведении и также автоматически скрывается через некоторое время, то в случае с `Popup` все эти действия нам надо задавать вручную.

Так, чтобы отразить при наведении мыши на элемент всплывающее окно, нам надо соответственным образом обработать событие `MouseEnter`.

Второй момент, который надо учесть, это установка свойства `StaysOpen="False"`. По умолчанию оно равно `True`, а это значит, что при отображении окна, оно больше не исчезнет, пока мы не установим явно значение этого свойства в `False`.

Итак, создадим всплывающее окно:
```xml
<StackPanel>
    <Button Content="Popup" Width="80" MouseEnter="Button_MouseEnter_1" HorizontalAlignment="Left" />
    <Popup x:Name="popup1" StaysOpen="False" Placement="Mouse" MaxWidth="180"
         AllowsTransparency="True"  >
        <TextBlock TextWrapping="Wrap" Width="180" Background="LightPink" Opacity="0.8" >
            Чтобы узнать больше, посетите сайт metanit.com
        </TextBlock>
    </Popup>
</StackPanel>
```

И обработчик наведения курсора мыши на кнопку в коде c#:
```cs
private void Button_MouseEnter_1(object sender, MouseEventArgs e)
{
    popup1.IsOpen = true;
}
```

И при наведении указателя мыши на элемент появится всплывающее окно с сообщением.

#### Скрытие окна
Если необходимо, чтобы `Popup` автоматически закрывался при клике вне его области, можно установить свойство `StaysOpen` в `false`:
```xml
<Popup StaysOpen="False">...</Popup>
```

Этот подход не требует использования дополнительного кода, но при этом `Popup` закроется при любом клике вне его области.

Чтобы скрыть `Popup` в WPF автоматически через определенное время, можно использовать таймер (`DispatcherTimer`) для установки свойства `IsOpen` в `false` после заданного интервала. Пример реализации с использованием обоих подходов показан ниже.

*XAML*:
```xml
<StackPanel>
    <Button x:Name="addBtn" Content="Add to Basket" Width="80" HorizontalAlignment="Left" Click="addBtn_Click" />
    <Popup x:Name="addedPopup" Placement="Mouse" MinHeight="50" MinWidth="200"
           HorizontalOffset="25" VerticalOffset="25" StaysOpen="False"
            AllowsTransparency="True" PopupAnimation="Fade"
    >
        <TextBlock TextWrapping="Wrap" Width="180" Background="LightPink" Opacity="0.5" >
            Товар добавлен в корзину!
        </TextBlock>
    </Popup>
</StackPanel>
```

*C#*:
```cs
private void addBtn_Click(object sender, RoutedEventArgs e)
{
    // Создание таймера
    DispatcherTimer timer = new DispatcherTimer();

    // Настройка таймера
    timer.Interval = TimeSpan.FromSeconds(5); // Скрыть через 5 секунд
    timer.Tick += (s, args) =>
    {
        addedPopup.IsOpen = false; // Скрыть Popup
        timer.Stop(); // Остановить таймер
    };

    // Запуск таймера при открытии Popup
    addedPopup.IsOpen = true;
    timer.Start();
}
```

Шаги:
1. **Создание Таймера**: Создается экземпляр `DispatcherTimer`.

2. **Настройка Интервала**: Устанавливается интервал, через который таймер сработает (`Interval`).

3. **Обработчик События `Tick`**: В обработчике события `Tick` устанавливается `IsOpen` в `false`, чтобы скрыть `Popup`, а таймер останавливается, чтобы он не срабатывал повторно.

4. **Запуск Таймера**: Запускается таймер сразу после открытия `Popup`.

### Image / Изображение
Элемент `Image` в WPF используется для отображения изображений в приложениях. Он поддерживает различные форматы изображений, включая *.bmp*, *.png*, *.gif*, *.jpg*, *.tiff*, *.ico*, и *.wdp*.

Определение:
```cs
[System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
public class Image : System.Windows.FrameworkElement, System.Windows.Markup.IUriContext
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.image?view=windowsdesktop-9.0

Свойство **`Source`** позволяет задать путь к изображению, например:
```xml
<Image Source="myPhoto.jpg" />
```

Путь может быть как локальным, так и сетевым. Соответственно, могут быть разные варианты указания источника изображения.

1. Отображение локального файла изображения:

    XAML:
    ```xml
    <Image Source="myPhoto.jpg" />
    ```

    CodeBehind:
    ```cs
    img.Source = new BitmapImage(new Uri("myPhoto.jpg", UriKind.Relative));
    ```

2. Отображение ресурса, встроенного в сборку:

    XAML:
    ```xml
    <Image Source="pack://application:,,,/images/myPhoto.jpg" />
    ```

    CodeBehind:
    ```cs
    img.Source = new BitmapImage(new Uri("pack://application:,,,/images/myPhoto.jpg"));
    ```

3. Отображение удаленного файла изображения:

    XAML:
    ```xml
    <Image Source="http://example.com/myPhoto.jpg" />
    ```

    CodeBehind:
    ```cs
    img.Source = new BitmapImage(new Uri("http://example.com/myPhoto.jpg"));
    ```

Как видно из отделенного кода, это свойство в реальности имеет тип `ImageSource`, который может быть представлен различными классами, такими как `BitmapImage`, `DrawingImage`, `ImageDrawing`, и другими. Из кода XAML это свойство транслируется благодаря использованию конвертера.

Типы `ImageSource`:
- `BitmapImage`: Используется для отображения растровых изображений. Предназначен для оптимизации загрузки изображений из XAML

- `DrawingImage`: Используется для отображения векторных рисунков. Он преобразует векторные команды рисования в растровое изображение

- `ImageDrawing`: Используется для отображения изображений в виде рисунков. Позволяет рисовать изображение (`ImageSource`) в определенном месте и размере. Он используется для включения изображений в векторные рисунки.

Свойство **`Stretch`** определяет, как изображение будет масштабироваться внутри элемента `Image`. Возможные значения:

- `None`: Изображение отображается в натуральном размере.

- `Uniform`: Масштабируется, сохраняя пропорции.

- `Fill`: Заполняет все пространство, не сохраняя пропорции.

- `UniformToFill`: Заполняет пространство, сохраняя пропорции

```xml
<Image Source="myPhoto.jpg" Stretch="Uniform" />
```

**`Clip`** позволяет вырезать определенную часть изображения с помощью объекта `RectangleGeometry`:
```xml
<Image Source="myPhoto.jpg">
    <Image.Clip>
        <RectangleGeometry Rect="20,20,300,250" />
    </Image.Clip>
</Image>
```

Также элемент позволяет проводить некоторые простейшие трансформации с изображениями. Например, с помощью объекта **`FormatConvertedBitmap`** и его свойства **`DestinationFormat`** можно получить новое изображение:
```xml
<Grid Background="Black">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="2.5*" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    <Image Grid.Column="0" x:Name="mainImage">
        <Image.Source>
            <FormatConvertedBitmap Source="3.jpg"
                DestinationFormat="Gray32Float" />
        </Image.Source>
    </Image>
    <StackPanel Grid.Column="1">
        <Image Source="1.jpg" />
        <Image Source="2.jpg" />
        <Image Source="4.jpg" />
        <Image Source="3.jpg" />
    </StackPanel>
</Grid>
```

Элемент `Image` можно создавать и настраивать программно, используя классы `BitmapImage` и `Uri` для задания источника изображения:
```cs
Assembly assembly = Assembly.GetExecutingAssembly();
var AssemblyName = assembly.ToString();

System.Windows.Controls.Image image = new System.Windows.Controls.Image();
BitmapImage bitmap = new BitmapImage();
bitmap.BeginInit();
bitmap.UriSource = new Uri($"pack://application:,,,/{AssemblyName};component/Images/wpf_logo.jpg");
bitmap.EndInit();
image.Source = bitmap;
image.Stretch = Stretch.Uniform;

stkPnl.Children.Add(image);
```

В этом примере изображение *wpf_logo.jpg* находится в папке *Images* в корне проекта. Для данного файл `BuildAction` обязательно должен быть установлен в `Resource` (это можно узнать из свойств объекта в панели *Properties*). Кроме того, в отдельных случаях может потребоваться установить свойство `CopyToOutputDirectory` в "Copy if newer" или "Copy always". Название сборки можно определить динамически, как показано выше, но, как правило, оно совпадает с названием проекта и указано в файле *Properties/AssemblyInfo.cs*:
```cs
...
[assembly: AssemblyTitle("Image")]
...
```

Ограничения:
- Элемент `Image` не поддерживает анимацию многокадровых изображений и не позволяет редактировать изображения напрямую. Для редактирования изображений можно использовать другие классы, такие как `WriteableBitmap` или `FormatConvertedBitmap`.

Элемент `Image` может быть использован внутри `InkCanvas`, что позволяет рисовать поверх изображения

### InkCanvas / Полотно
`InkCanvas` в WPF — это элемент управления, который позволяет пользователям создавать и редактировать рукописные аннотации с помощью стилуса или мыши. Он обеспечивает поддержку перьевого ввода и может быть использован для создания приложений, требующих рукописного ввода, таких как цифровые доски или графические редакторы.

Определение:
```cs
[System.Windows.Markup.ContentProperty("Children")]
public class InkCanvas : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild
```

Описание: https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.inkcanvas?view=windowsdesktop-9.0

`InkCanvas` представляет собой полотно, на котором можно рисовать. Первоначально оно предназначалось для стилуса, но в WPF есть поддержка также и для мыши для обычных ПК. Его очень просто использовать:
```xml
<InkCanvas Background="LightCyan" />
```

Либо мы можем вложить в `InkCanvas` какое-нибудь изображение и на нем уже рисовать:
```xml
<InkCanvas>
    <Image Source="2.jpg"  Width="300" Height="250"  />
</InkCanvas>
```

Все рисование в итоге представляется в виде штрихов — элементов класса `System.Windows.Ink.Stroke` и хранится в коллекции `Strokes`, определенной в классе `InkCanvas`.

Таким образом, элемент управления `InkCanvas` позволяет рисовать и редактировать линии с помощью мыши или пера. Размеры элемента управления можно задать с помощью свойств `Width` и `Height`. Свойства пера (цвет, ширину и высоту) можно настроить с помощью свойства `DefaultDrawingAttributes`:
```xml
<InkCanvas>
  <InkCanvas.DefaultDrawingAttributes>
    <DrawingAttributes Color="Red" Height="10" Width="1"/>
  </InkCanvas.DefaultDrawingAttributes>
</InkCanvas>
```

Свойство `EditingMode` позволяет настроить режим редактирования: рисование (`Ink`), выбор и редактирование фигур (`Select`), удаление по точкам (`EraseByPoint`) и удаление фигур (`EraseByStroke`).

Основные особенности:
- **Рисование и редактирование**: `InkCanvas` позволяет пользователям рисовать штрихи с помощью мыши или стилуса. Эти штрихи хранятся в коллекции `Strokes`, которая может быть легко манипулирована программно.

- **Режимы редактирования**: `InkCanvas` поддерживает несколько режимов редактирования, определяемых свойством `EditingMode`.

- **События**: `InkCanvas` предоставляет события, такие как `StrokeCollected` (штрих нарисован) и `StrokeErased` (штрих стерт), что позволяет разработчикам контролировать процесс рисования и редактирования.

- **Вложение элементов**: В `InkCanvas` можно вложить другие элементы, такие как изображения, что позволяет рисовать аннотации поверх них.

Пример создания простого `InkCanvas` с помощью XAML:
```xml
<InkCanvas Name="myInkCanvas" Background="LightCyan" />
```

В этом примере создается `InkCanvas` с светло-голубым фоном, на котором пользователь может рисовать штрихи.

Основные свойства и их описание:

1. **`DefaultDrawingAttributes`**: Определяет атрибуты рисования, которые применяются к новым штрихам, создаваемым на `InkCanvas`. Это включает в себя цвет, толщину и другие свойства штрихов.

    ```cs
    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
    ```

2. **`EditingMode`**: Управляет режимом редактирования `InkCanvas`. Возможные режимы: `Ink`, `InkAndGesture`, `GestureOnly`, `EraseByStroke`, `EraseByPoint`, `Select`, `None`.

    ```cs
    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
    ```

3. **`Strokes`**: Коллекция штрихов, которые были нарисованы на `InkCanvas`. Это свойство позволяет управлять существующими штрихами.

    ```cs
    inkCanvas.Strokes.Clear(); // для очистки всех штрихов.
    ```

4. **`Background`**: Определяет фон `InkCanvas`. Может быть цветом или изображением.

    ```xml
    <InkCanvas Background="LightCyan" />
    ```

5. **`Top`**, **`Left`**, **`Bottom`**, **`Right`**: Эти свойства используются для позиционирования дочерних элементов внутри `InkCanvas`, подобно `Canvas`.

    ```xml
    <InkCanvas>
        <Image InkCanvas.Left="10" InkCanvas.Top="20" />
    </InkCanvas>
    ```

Преимущества использования:
- **Гибкость**: `InkCanvas` может быть использован в различных приложениях, где требуется рукописный ввод.

- **Легкость использования**: Пользователи могут легко рисовать и редактировать штрихи с помощью мыши или стилуса.

- **Программный контроль**: Разработчики могут манипулировать штрихами программно, что позволяет создавать сложные приложения для редактирования и анализа рукописного ввода.

#### Режимы рисования
`InkCanvas` имеет несколько режимов, они задаются с помощью свойства **`EditingMode`**, значения для которого берутся из перечисления **`InkCanvasEditingMode`**. Эти значения бывают следующими:

- **`Ink`** Рисование штрихов: используется по умолчанию и предполагает рисование стилусом или мышью

- **`InkAndGesture`** Рисование штрихов и распознавание жестов: рисование с помощью мыши/стилуса, а также с помощью жестов (Up, Down, Tap и др.)

- **`GestureOnly`** Только распознавание жестов: рисование только с помощью жестов пользователя

- **`EraseByStroke`** Стирание штрихов целиком: стирание всего штриха стилусом

- **`EraseByPoint`** Стирание части штриха: стирание только части штриха, к которой прикоснулся стилус

- **`Select`** Выделение штрихов: выделение всех штрихов при касании

- **`None`** Отключение любых действий: отсутствие какого-либо действия

Используя эти значения и обрабатывая события `InkCanvas`, такие как `StrokeCollected` (штрих нарисован), `StrokeErased` (штрих стерли) и др., можно управлять набором штрихов и создавать более функциональные приложения на основе `InkCanvas`.

#### Настройка внешнего вида
`InkCanvas` позволяет настраивать свой фон, например, с помощью градиента:
```xml
<InkCanvas Name="myInkCanvas">
    <InkCanvas.Background>
        <LinearGradientBrush>
            <GradientStop Color="Yellow" Offset="0.0" />
            <GradientStop Color="Blue" Offset="0.5" />
            <GradientStop Color="HotPink" Offset="1.0" />
        </LinearGradientBrush>
    </InkCanvas.Background>
</InkCanvas>
```

Этот пример создает градиентный фон для `InkCanvas`.

#### Сохранение и загрузка

*[ISF]: Ink Serielized Format
В WPF результаты работы пользователя с `InkCanvas` обычно сохраняются в виде сериализованного формата рукописного ввода (ISF). Вот основные шаги для сохранения и загрузки данных из `InkCanvas`:

1. Сохранение Данных из `InkCanvas`

    Чтобы сохранить данные из `InkCanvas`, вы можете использовать метод `Save()` класса `StrokeCollection`, который возвращает коллекцию штрихов (`Strokes`) из `InkCanvas`. Эти данные можно сохранить в файл или базу данных.

    ```cs
    // Создание диалогового окна для сохранения файла
    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
    saveFileDialog1.Filter = "isf files (*.isf)|*.isf";

    if (saveFileDialog1.ShowDialog() == true)
    {
        // Сохранение данных в файл
        using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create))
        {
            inkCanvas.Strokes.Save(fs);
        }
    }
    ```

2. Загрузка Данных в `InkCanvas`

    Для загрузки данных обратно в `InkCanvas`, вы можете использовать конструктор `StrokeCollection`, который принимает поток данных.

    ```cs
    // Создание диалогового окна для открытия файла
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Filter = "isf files (*.isf)|*.isf";

    if (openFileDialog1.ShowDialog() == true)
    {
        // Загрузка данных из файла
        using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
        {
            inkCanvas.Strokes = new StrokeCollection(fs);
        }
    }
    ```

3. Сохранение в Базу Данных

    Если вы хотите сохранить данные в базу данных, вы можете сериализовать `StrokeCollection` в двоичные данные (BLOB) и сохранить их в базе данных.

    ```cs
    using (MemoryStream ms = new MemoryStream())
    {
        inkCanvas.Strokes.Save(ms);
        byte[] binaryData = ms.ToArray();

        // Сохранение двоичных данных в базу данных
        using (var context = new MyDbContext())
        {
            var document = new Document { BinaryContent = binaryData };
            context.Documents.Add(document);
            context.SaveChanges();
        }
    }
    ```

4. Загрузка из Базы Данных

    Для загрузки данных из базы данных, вы можете прочитать двоичные данные и создать из них `StrokeCollection`.

    ```cs
    // Загрузка двоичных данных из базы данных
    using (var context = new MyDbContext())
    {
        var document = context.Documents.First();
        byte[] binaryData = document.BinaryContent;

        using (MemoryStream ms = new MemoryStream(binaryData))
        {
            inkCanvas.Strokes = new StrokeCollection(ms);
        }
    }
    ```

Таким образом, вы можете эффективно сохранять и загружать данные из `InkCanvas` в WPF, используя сериализованный формат ISF.
