Global HotKey.NET
===
ウィンドウが非アクティブな時でもキー入力を受けつけイベントを発生させる(Global hotkey)を扱うためのC\#ライブラリです。

This is a library for C# which enables your applications to executing some proccessing when the main window isn't active(global hotkey evenets).

#使い方/Usage
binディレクトリ内のGlobalHotKey.dllをプロジェクトの参照に追加してください。HotKey.HotKeyRegisterクラスを用いることで、グローバルホットキーイベントを発生させることが出来ます。
HotKeyRegisterコンストラクタは、引数に修飾キー、キー、そしてウィンドウを取ります。実際に行う処理は、HotKeyPressedイベントに登録します。

Add bin/GlobalHotKey.dll to a project referece. Using Hotkey.HotKeyRegister class, you can handle and raise global hotkey events.HotKeyRegister Constaructor take three arguments: modifyer key code, key code, and Window object. To implement what to do when hotkey pressed, you add events to HotKeyRegister.HotKeyPressed event.

#使用例/Sample
次の例はAlt+Kを押すとメッセージウィンドウを表示するサンプルです。
Following code is a sample, which show message windows when Alt +K pressed

```
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var hotkey = new HotKeyRegister(MOD_KEY.ALT, System.Windows.Forms.Keys.K, this);
        hotkey.HotKeyPressed += (sender) =>
        {
            MessageBox.Show("Key Pressed");
        };
    }
}
```

#Licence
MIT

##Auther
[mok-aster](https://github.com/mok-aster)]
