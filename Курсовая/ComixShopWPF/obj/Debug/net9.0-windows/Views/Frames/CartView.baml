   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ComixShopWPF.Views.FramesComixShopWPF.Views.Frames  5      A%clr-namespace:ComixShopWPF.ViewModelsComixShopWPF.ViewModels  5      (    "ComixShopWPF.Views.Frames.CartView   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:ComixShopWPF.Views.Frames     5      4vm%clr-namespace:ComixShopWPF.ViewModels     5        � DataContext  5
      +   %ComixShopWPF.ViewModels.CartViewModel  5   
   .��5      � .+�5   
   �� $��Ваша корзина��6   $��24(�5      $��Center=�5      $��0,0,0,10q�5      $
��Bold"�5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns$ False��5         CanUserAddRows$ False��5         CanUserDeleteRows$ False��5         
IsReadOnly$
 True��5      $��	0,50,0,70q�5      ��5      �� *	CartItems+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Comix.Title+6W     5      $ Количество��6%   $	 100 6V    69   �� *
Quantity+6V     5      $ Цена за штуку��6%   $	 120 6l    6<   �� *Comix.Price+	 �� StringFormat$	 C��6l   4  .System.Windows.Controls.DataGridTemplateColumn  5#      $ Действия��6)   $ 90 6;   
   CellTemplate
 5$      �� 5%      .R�5&      �� $��Удалить��6%   
���  ��
�������$	��5,0q�5+   %   ��5'   %   �� *DataContext.RemoveItemCommand+ �� RelativeSource ��  �� AncestorType DataGrid��5(   %   �� 5+   %   5,      5.      50      �� 52   
   $��15q�6    �� Orientation$ 
Horizontal=�6!   $��Right=�6:   $��Bottom=�6V   .+�53      �� $%��Общая стоимость: ��6   $
��Bold"�61   $��Center=�6C   $��0,0,10,0q�6^   �� 54      $
��Bold"�6C   $��Center=�6U   $��0,0,20,0q�6p   ��6   �� *	TotalCost+$	 C��6p   �� 56      $%��Очистить корзину��6   
���   �
�������
���   �$	��150��5;      $��40��5<      $
��10,0q�5=      ��57      �� *ClearCartCommand+5=      �� 5?      $!��Оформить заказ��6   $	��150��5A      $��40��5B      $
��10,0q�5C      ��5@      �� *PlaceOrderCommand+5C      5D      5E      5F      