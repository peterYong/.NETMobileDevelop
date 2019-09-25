# Xamarin.Forms开发

### 1、AwesomeApp ###

第一个app



### 2、Notes ###

增删改查 数据显示



### 3、Practice ###

平时练习，Views中的各个页面如下：

#### 数据绑定 ####

1、BasicCodeBindingPage【使用BindingContext】

仅在cs中实现绑定

2、BasicXamlBindingPage【使用BindingContext】

仅在Xaml中实现绑定

BindingContext属性设置为x：Reference（源）

3、AlternativeCodeBindingPage【不使用BindingContext】

直接在SetBinding的方法参数Binding中指定源对象

4、AlternativeXamlBindingPage【不使用BindingContext】

Binding的Source属性 绑定到x：Reference（源）

5、BindingContextInheritancePage

绑定上下文继承，父布局中定义了源对象，子控件中都可以使用

6、ReverseBindingPage

反向的：现在，Label 是数据绑定源，而 Slider 是目标。【因为上面几个的Slider的Value默认值为0，初始不显示】

Slider绑定Label的Opacity属性，其默认值为1，初始会显示。

7、SimpleColorSelectorPage

使用ViewModels来作为Source，绑定到目标（views）

8、SampleSettingsViewModel

替换掉默认的绑定模式的例子：

一种非常有用的应用程序涉及 ListView 的 SelectedItem 属性。，其默认绑定模式为 OneWayToSource（目标到源）

我们将其改为TwoWay。

9、PathVariationsPage

绑定路径Path的一些设置，，设置为属性的属性/集合的元素

#### 用户界面 ####

1、ActivityIndicatorPage

Activity指示器，类似进度条

2、显示弹出窗口

先添加一个选项卡式页：DisplayPopUps

再添加两个内容页：Alert、action sheet

采用TabbedPage导航。

xmlns:local="clr-namespace:FromPractice.Views.UserInterface;assembly=FromPractice" 

注意：

xmlns:local 是指引用本地的

clr-namespace 是命名空间全名

assembly 是程序集


