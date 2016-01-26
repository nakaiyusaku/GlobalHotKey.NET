Global HotKey.NET
===
�E�B���h�E����A�N�e�B�u�Ȏ��ł��L�[���͂��󂯂��C�x���g�𔭐�������(Global hotkey)���������߂�C\#���C�u�����ł��B

This is a library for C# which enables your applications to executing some proccessing when the main window isn't active(global hotkey evenets).

#�g����/Usage
bin�f�B���N�g������GlobalHotKey.dll���v���W�F�N�g�̎Q�Ƃɒǉ����Ă��������BHotKey.HotKeyRegister�N���X��p���邱�ƂŁA�O���[�o���z�b�g�L�[�C�x���g�𔭐������邱�Ƃ��o���܂��B
HotKeyRegister�R���X�g���N�^�́A�����ɏC���L�[�A�L�[�A�����ăE�B���h�E�����܂��B���ۂɍs�������́AHotKeyPressed�C�x���g�ɓo�^���܂��B

Add bin/GlobalHotKey.dll to a project referece. Using Hotkey.HotKeyRegister class, you can handle and raise global hotkey events.HotKeyRegister Constaructor take three arguments: modifyer key code, key code, and Window object. To implement what to do when hotkey pressed, you add events to HotKeyRegister.HotKeyPressed event.

#�g�p��/Sample
���̗��Alt+K�������ƃ��b�Z�[�W�E�B���h�E��\������T���v���ł��B
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
