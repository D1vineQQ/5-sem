   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ComixShopWPF.Views.FramesComixShopWPF.Views.Frames  5      A%clr-namespace:ComixShopWPF.ViewModelsComixShopWPF.ViewModels  5      +    %ComixShopWPF.Views.Frames.CatalogView   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:ComixShopWPF.Views.Frames     5      4vm%clr-namespace:ComixShopWPF.ViewModels     5        � DataContext  5
      .   (ComixShopWPF.ViewModels.CatalogViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $��5* �6   6,   .+�5   
   �� $��	10 0 10 0q�6   $��0��6(   .+�5      �� $��14(�6   $��0 10 0 0q�6&   $��Center=�68   
Поиск6\   �� 5      -   5         ��Name$ 	SearchBox��5   )   $��	0 10 0 10q�6   ��6<   �� *	SearchBox+�� 5      $��14(�6   $��Center=�6&   $��0,10,0,0q�6C   $��Жанр��6U   6c   �� 5      -   5         ��Name$ FilterCombo��5      $��	0,10,0,10q�5      $��12(�5      ��5      �� *
FilterText+ �� SelectedItem 5      �� *SelectedGenre+ �� Mode$ TwoWay=�5      �� 5      $��Center=�5       $��15(�5!      $��Цена��5"      ${�False��5#      �� 5%       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5&      �� -   5        $ 	FromPrice��5&      $��35��6-   $��9q�6;   ��6F   �� *PriceDiapLeft+�� 5'      $��Center=�6   -6<   �� 5(      -   5        $ ToPrice��5(      $��35��6+   $��9q�69   ��6D   �� *PriceDiapRight+5)      �� 5*      $��	10 0 10 0q�6   ��6(   �� *SearchComixCommand+.��6O   
Найти6V   �� 5+      
�������
���   �$��
10 15 10 0q�6<   ��6P   �� *ResetComixCommand+.��6v   Reset6}   5,      �� 5-   
   $��Bottom=�6   .+�5.      �� $��
10 0 10 15q�6   ��6)   �� *AddToCartCommand+.��6N   В корзину6Y   �� 5/      $��
10 0 10 10q�6   ��6)   �� *AddMultipleToCartCommand+.��6V   Несколько6a   50      &   System.Windows.Controls.DataGrid  51   
   -   5          Name$ ComixDataGrid��57      $��1��54         SelectionMode$ Single=�55      	   
IsReadOnly$
	 True��56      
   AutoGenerateColumns$
 False��58      $��False��59      $
��Auto=�5:       52      �� *SelectedComix+$ TwoWay=���54   !   �� *	ComixList+   Columns 5;      4  .System.Windows.Controls.DataGridTemplateColumn  5<      -   5        	dgImg��,  &System.Windows.Controls.DataGridColumn   Header$ Image��5<   8      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.5* 6G      MinWidth$	 100Q�6T      CellTemplate 5=      �� 5>      .R�5?      �� ��6%   �� *
ImageUrl+5A      5C      0  *System.Windows.Controls.DataGridTextColumn  5D      -   5        	dgTtl��$ Title��5D   4   $
 1.5* 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   ��  �� Path$ Title�   ElementStyle 5E      �� 5F       �� 
TargetType" ��6    .�5G      ��  �� Property � �� �� Value$
 Wrap=�6=   �� 5H       � ��$ WordEllipsis=�6=   5I      5K        5L      -   5        	dgGnr��$ Genre��5L   4   $ * 6b    6C   �� $ Genre� 5M      �� 5N      " ��6    .�5O      ��  � ��$
 Wrap=�6=   �� 5P       � ��$ WordEllipsis=�6=   5Q      5S        5T      -	   5        
dgAthr��$ Author��5T   5   $ * 6e    6E   �� $ Author� 5U      �� 5V      " ��6    .�5W      ��  � ��$
 Wrap=�6=   �� 5X       � ��$ WordEllipsis=�6=   5Y      5[        5\      -
   5        	dgPrc��$ Price��5\   4   $
 0.5* 6b    6C   �� $ Price�6b     5]      -   5        
dgStck��$ Quantity��5]   5   $
 0.5* 6n    6G   �� $ StockQuantity�6n   5_      5`      5a      