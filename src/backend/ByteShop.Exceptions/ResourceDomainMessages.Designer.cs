﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ByteShop.Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceDomainMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceDomainMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ByteShop.Exceptions.ResourceDomainMessages", typeof(ResourceDomainMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apenas 3 niveis de categoria são permitida..
        /// </summary>
        public static string MAXIMUM_CATEGORY_LEVEL {
            get {
                return ResourceManager.GetString("MAXIMUM_CATEGORY_LEVEL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to É necessario ter uma imagem principal, para adicionar uma imagem segundaria..
        /// </summary>
        public static string MUST_HAVE_A_MAIN_IMAGE {
            get {
                return ResourceManager.GetString("MUST_HAVE_A_MAIN_IMAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Existe produtos associado a categoria..
        /// </summary>
        public static string THERE_IS_A_PRODUCT_ASSOCIATED_WITH_THE_CATEGORY {
            get {
                return ResourceManager.GetString("THERE_IS_A_PRODUCT_ASSOCIATED_WITH_THE_CATEGORY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não é possível excluir esta categoria porque ela tem categorias filhas vinculadas.
        /// </summary>
        public static string THERE_IS_AN_ASSOCIATED_CHILD_CATEGORY {
            get {
                return ResourceManager.GetString("THERE_IS_AN_ASSOCIATED_CHILD_CATEGORY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Você não pode alterar a categoria pai para uma categoria pai de um nível diferente. Por favor, escolha uma categoria pai com o mesmo nível ou altere o nível da categoria selecionada..
        /// </summary>
        public static string UPDATE_PARENT_CATEGORY_BY_DIFFERENT_LEVEL {
            get {
                return ResourceManager.GetString("UPDATE_PARENT_CATEGORY_BY_DIFFERENT_LEVEL", resourceCulture);
            }
        }
    }
}
