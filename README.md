# NotifyErrorInfoSample

Repro for https://github.com/dotnet/wpf/pull/1167

If you edit MainWindow.xaml and set Expander property  `IsExpanded=true`, you'll see the validation adorner properly rendered.

However, if it is set to false, then the validation adorner is not visible. 