   M S B A M L   `   `   `  ����F  BMyHomeLibUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MyHomeLibUIMyHomeLibUI  5          MyHomeLibUI.FB2Page   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:MyHomeLibUI     5      .��5	      � -   5          �Name$  pageGrid��5	       � RowDefinitions 5
   
   �� 5      $
��Auto �6   5      �� 5      $
��Auto �6   5      �� 5      5      .+�5   
   �� $��Select Source for Import��6   $��5q�64   $
��Bold"�6?   6S   � 5   
   -   5        $  Source��5      $��1��6    5      �� 5      $
��Auto �6!   5      �� 5      $
��Auto �6!   5      �� 5      $
��Auto �6!   5       � ColumnDefinitions 5!      �� 5"      $
��Auto �6#   62   �� 5#      6%   .+�5%      	� -   5         	�Name$ LibDirectory��5%       	� 	GroupName$ 
SourceType��60   $��Select Directory��6G    d� 	IsChecked$
 TrueY�5&      $��5q�6+   $��0��66   $��0��6F   6t      MyHomeLibUI.SelectPath  5'      -   5          Name$ SelectLibDir��5'         MyHomeLibUI.SelectPath   PathChanged$ DirPath_PathChanged��5(   8   $��10,0,0,0q�5'   6   $��1��6H   $��0��6X      	LabelText$  ��5(      	   SelectionType$	 1��6&   6\   	� 5*      -   5        $ LibFile��5*      $ 
SourceType��6,   $��Select File��6C   $��5q�5+      $��0��6&   $��1��66   6_     5,      -   5        $ 
SelectFile��5,      $ FilePath_PathChanged��5-   >   $��10,0,0,0q�5,   3   $��1��6E   $��1��6U   $  ��5-      $	 2��6,   
   Filter$G
 ALibrary files files (*.zip;*.fb2)|*.zip;*.fb2|All files (*.*)|*.*��5.      5/      	� 51      -   5        $ LibBook��51      $ 
SourceType��6,   $��Select Book��6C   $��5q�52      $��0��6&   $��2��66   6_     53      -   5        $ 
SelectItem��53      $ ItemPath_PathChanged��54   >   $��10,0,0,0q�53   3   $��1��6E   $��2��6U   $  ��54      $	 2��6,   $G
 ALibrary files files (*.zip;*.fb2)|*.zip;*.fb2|All files (*.*)|*.*��55      56      57      R� 59   
   -	   5         R�Name$ 	ItemsTree��59      $��2��6&   $��5q�63   5:      5;      