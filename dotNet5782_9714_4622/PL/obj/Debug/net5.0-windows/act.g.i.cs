﻿#pragma checksum "..\..\..\act.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "182C5AC32554ABA7E74472E478FBD4DF4BE18217"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace PL {
    
    
    /// <summary>
    /// act
    /// </summary>
    public partial class act : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\act.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dronView;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\act.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button stationView;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\act.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button customerView;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\act.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button parcelView;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\act.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/act.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\act.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dronView = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\act.xaml"
            this.dronView.Click += new System.Windows.RoutedEventHandler(this.dronView_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.stationView = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\act.xaml"
            this.stationView.Click += new System.Windows.RoutedEventHandler(this.stationView_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.customerView = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\act.xaml"
            this.customerView.Click += new System.Windows.RoutedEventHandler(this.customerView_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.parcelView = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\act.xaml"
            this.parcelView.Click += new System.Windows.RoutedEventHandler(this.parcelView_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.close = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\act.xaml"
            this.close.Click += new System.Windows.RoutedEventHandler(this.close_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

