   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:ComixShopWPF.Views.Frames.AdminViews$ComixShopWPF.Views.Frames.AdminViews  5      a5clr-namespace:ComixShopWPF.ViewModels.AdminViewModels'ComixShopWPF.ViewModels.AdminViewModels  5      =    7ComixShopWPF.Views.Frames.AdminViews.AdminManagmentView   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:ComixShopWPF.Views.Frames.AdminViews     5      Fadvm5clr-namespace:ComixShopWPF.ViewModels.AdminViewModels     5        � DataContext  5
      C   =ComixShopWPF.ViewModels.AdminViewModels.AdmManagmentViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   6,   �� 5      $��5* �6   6,   .+�5   
   �� $��Bottom=�6   .+�5      �� $��
10 0 10 15q�6   ��6)   �� *AddComixCommand+.��6M   Добавить6W   �� 5      $��
10 0 10 15q�6   ��6)   �� *EditComixCommand+.��6N   Изменить6X   �� 5      $��
10 0 10 10q�6   ��6)   �� *DeleteComixCommand+.��6P   Удалить6Y   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ComixDataGrid��5      $��1��5         SelectionMode$ Single=�5         
IsReadOnly$
 True��5         AutoGenerateColumns$ False��5      $��False��5      $
��Auto=�5        �� SelectedItem 5      �� *SelectedComix+ �� Mode$ TwoWay=���5      �� *	ComixList+   Columns 5!      4  .System.Windows.Controls.DataGridTemplateColumn  5"      -   5        	dgImg��,  &System.Windows.Controls.DataGridColumn	   Header$	 Image��5"   8   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 1.5* 6G      MinWidth$	 100Q�6T      CellTemplate 5#      �� 5$      .R�5%      �� ��6%   �� *
ImageUrl+5'      5)      0  *System.Windows.Controls.DataGridTextColumn  5*      -   5        	dgTtl��$	 Title��5*   4   $

 1.5* 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   ��  �� Path$ Title�   ElementStyle 5+      �� 5,       �� 
TargetType" ��6    .�5-      ��  �� Property � �� �� Value$
 Wrap=�6=   �� 5.       � ��$ WordEllipsis=�6=   5/      51        52      -   5        	dgGnr��$	 Genre��52   4   $
 * 6b    6C   �� $ Genre� 53      �� 54      " ��6    .�55      ��  � ��$
 Wrap=�6=   �� 56       � ��$ WordEllipsis=�6=   57      59        5:      -   5        
dgAthr��$	 Author��5:   5   $
 * 6e    6E   �� $ Author� 5;      �� 5<      " ��6    .�5=      ��  � ��$
 Wrap=�6=   �� 5>       � ��$ WordEllipsis=�6=   5?      5A        5B      -   5        	dgPrc��$	 Price��5B   4   $

 0.5* 6b    6C   �� $ Price�6b     5C      -   5        
dgStck��$	 Quantity��5C   5   $

 0.5* 6n    6G   �� $ StockQuantity�6n   5E      5F      5G      