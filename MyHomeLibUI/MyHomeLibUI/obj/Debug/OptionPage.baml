   M S B A M L   `   `   `  ����F  BMyHomeLibUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MyHomeLibUIMyHomeLibUI  5          MyHomeLibUI.OptionPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:MyHomeLibUI     5      .��5	      � -   5          �Name$  pageGrid��5	       � RowDefinitions 5
   
   �� 5      $
��Auto �6    5      �� 5      $
��Auto �6    5      �� 5      $
��Auto �6    5      .+�5   
   �� $��Select Options��6   $��5q�6*   $
��Bold"�65   6I   ]� 5   
   -   5         ]�Name$ NewDB��5      ��. $��1��68   .+�5      !   MyHomeLibUI.SelectParamText  -   5          Name$ 	NewDBName��5   $   !   MyHomeLibUI.SelectParamText   TextChanged$ NewDBName_TextChanged��5   >   $��Bottom=�5   7   $��40,0,0,0q�6O      	LabelText$ New Data Base��5   $   6d      MyHomeLibUI.SelectPath  5      -   5          Name$ 	NewDBPath��5         MyHomeLibUI.SelectPath   PathChanged$ NewDBPath_PathChanged��5   I   $��Bottom=�5   2   $��40,0,0,0q�6J      	LabelText$ Select Path��5      	   SelectionType$	 1��67   6o   	� 5      -   5        
 	�Name$
 Create��5       	� 	GroupName$ 
UpdateType��6*   $��Crete new database��6A    d� 	IsChecked$
 TrueY�6^   $��5q�6p   $��Bottom=�6{   6�   5      ]� 5   
   -   5        $ 
SelectedDB��5      ��. $��2��6=   .+�5        -   5        $ SelectedDBPath��5      $  SelectedDBPath_PathChanged��5   L   $��Bottom=�5   7   $��40,0,0,0q�6O   $ Select DataBase��5      $	 2��6;   {�.     Filter$) #Data Base files (*.sqlite)|*.sqlite��5      6w   	� 5      -   5        $
 Update��5      $ 
UpdateType��6+   $��Update selected database��6B   $��5q�6e   $��Bottom=�6p   6�   5      5       5!      