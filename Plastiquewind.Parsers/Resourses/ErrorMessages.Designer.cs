﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plastiquewind.Parsers.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Stecpoint.Import.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка Excel в ячейке {0} в листе {1} - поле {2}: {3}..
        /// </summary>
        internal static string XlsxCellValueError {
            get {
                return ResourceManager.GetString("XlsxCellValueError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка в ячейке {0} в листе {1} - поле {2}: неверный формат данных..
        /// </summary>
        internal static string XlsxDataTypeError {
            get {
                return ResourceManager.GetString("XlsxDataTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Импортируемый файл не содержит необходимых заголовков..
        /// </summary>
        internal static string XlsxMissingHeaderError {
            get {
                return ResourceManager.GetString("XlsxMissingHeaderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка формата файла. Файл должен быть формата Excel 2007.xlsx.
        /// </summary>
        internal static string XlsxOpeningError {
            get {
                return ResourceManager.GetString("XlsxOpeningError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Файл в режиме защищённого просмотра. Откройте файл в Excel, нажмите кнопку &quot;Разрешить редактирование&quot;, сохраните файл и загрузите снова..
        /// </summary>
        internal static string XlsxProtectedViewError {
            get {
                return ResourceManager.GetString("XlsxProtectedViewError", resourceCulture);
            }
        }
    }
}
