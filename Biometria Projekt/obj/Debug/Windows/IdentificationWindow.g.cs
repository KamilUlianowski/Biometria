﻿#pragma checksum "..\..\..\Windows\IdentificationWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F7B443F46E8366999F4B8F6E3E60BCF4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Biometria_Projekt.Windows;
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


namespace Biometria_Projekt.Windows {
    
    
    /// <summary>
    /// IdentificationWindow
    /// </summary>
    public partial class IdentificationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Windows\IdentificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberOfSamples;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Windows\IdentificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConfirmButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Windows\IdentificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserText;
        
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
            System.Uri resourceLocater = new System.Uri("/Biometria Projekt;component/windows/identificationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\IdentificationWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.NumberOfSamples = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ConfirmButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Windows\IdentificationWindow.xaml"
            this.ConfirmButton.Click += new System.Windows.RoutedEventHandler(this.ConfirmButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.UserText = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\Windows\IdentificationWindow.xaml"
            this.UserText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.UserText_TextChanged);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\Windows\IdentificationWindow.xaml"
            this.UserText.KeyDown += new System.Windows.Input.KeyEventHandler(this.UserText_OnKeyDown);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\Windows\IdentificationWindow.xaml"
            this.UserText.KeyUp += new System.Windows.Input.KeyEventHandler(this.UserText_OnKeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

