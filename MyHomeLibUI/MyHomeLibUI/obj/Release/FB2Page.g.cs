﻿#pragma checksum "..\..\FB2Page.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B06F19517FA6823DBF3E85B38676BE5CAAA91E3CC2ADB45F12E5DE102C95237C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MyHomeLibUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MyHomeLibUI {
    
    
    /// <summary>
    /// FB2Page
    /// </summary>
    public partial class FB2Page : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pageGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Source;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton LibDirectory;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyHomeLibUI.SelectPath SelectLibDir;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton LibFile;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyHomeLibUI.SelectPath SelectFile;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton LibBook;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyHomeLibUI.SelectPath SelectItem;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\FB2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView ItemsTree;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyHomeLibUI;component/fb2page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FB2Page.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pageGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Source = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.LibDirectory = ((System.Windows.Controls.RadioButton)(target));
            
            #line 38 "..\..\FB2Page.xaml"
            this.LibDirectory.Checked += new System.Windows.RoutedEventHandler(this.LibDirectory_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SelectLibDir = ((MyHomeLibUI.SelectPath)(target));
            return;
            case 5:
            this.LibFile = ((System.Windows.Controls.RadioButton)(target));
            
            #line 43 "..\..\FB2Page.xaml"
            this.LibFile.Checked += new System.Windows.RoutedEventHandler(this.LibFile_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SelectFile = ((MyHomeLibUI.SelectPath)(target));
            return;
            case 7:
            this.LibBook = ((System.Windows.Controls.RadioButton)(target));
            
            #line 50 "..\..\FB2Page.xaml"
            this.LibBook.Checked += new System.Windows.RoutedEventHandler(this.LibBook_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SelectItem = ((MyHomeLibUI.SelectPath)(target));
            return;
            case 9:
            this.ItemsTree = ((System.Windows.Controls.TreeView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

