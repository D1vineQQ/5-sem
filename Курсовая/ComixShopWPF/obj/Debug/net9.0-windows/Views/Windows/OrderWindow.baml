   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ComixShopWPF.Views.WindowsComixShopWPF.Views.Windows  5   	   A%clr-namespace:ComixShopWPF.ViewModelsComixShopWPF.ViewModels  5   	   ,    &ComixShopWPF.Views.Windows.OrderWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ComixShopWPF.Views.Windows     5   	   4vm%clr-namespace:ComixShopWPF.ViewModels     5   	     � DataContext  5
      2   ,ComixShopWPF.ViewModels.OrderWindowViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $
��0.5* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   5      �� 5   
   $-��'Подтверждение заказа��6   $��24(�5      $��Center=�5      $��0,0,0,10q�5      $
��Bold"�5      &   System.Windows.Controls.DataGrid  5   
   $��0��5   	      AutoGenerateColumns$ False��5   	      CanUserAddRows$ False��5    	      CanUserDeleteRows$ False��5!   	      
IsReadOnly$
 True��5"   	   $��0,50,0,0q�5#   	   ��5   	   �� *	CartItems+   Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6W   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   �� *Comix.Title+6W     5'      $ Количество��6%   $		 100 6V   
 69   �� *
Quantity+6V     5(      $ Цена за штуку��6%   $		 120 6l   
 6<   �� *Comix.Price+ �� StringFormat$ C��6l   5-      �� 5/   
   $ Vertical=�6   $��1��6,   .+�50      �� $1��+Выберите способ оплаты:��6   $
��Bold"�66   �� 51      $��
50,0,100,0q�53      $
��Left=�54      $	��150��55      ��51      �� *PaymentMethods+ �� SelectedItem 52      �� *SelectedPaymentMethod+55      �� 58      $J��DКомментарий к заказу (необязательно):��6   $
��Bold"�6D   �� 59      $��	50 0 50 0q�6   $��60��6G    �� TextWrapping$
 Wrap=�6S   $	��400��6g   $
��Left=�6s   ��6)   �� *OrderComment+6s   5:      �� 5;   
   $��2��6   $��15q�6"   $ 
Horizontal=�6.   $��Right=�6G   $��Bottom=�6c   .+�5<      �� $%��Общая стоимость: ��6   $
��Bold"�61   $��Center=�6C   $��0,0,10,0q�6^   �� 5=      $
��Bold"�6C   $��Center=�6U   $��0,0,20,0q�6p   ��6   �� *	TotalCost+$ C��6p   �� 5@      $!��Оформить заказ��6   $	��150��5B      $��40��5C      $
��10,0q�5D      ��5A      �� *PlaceOrderCommand+5D      5E      5F      5G      