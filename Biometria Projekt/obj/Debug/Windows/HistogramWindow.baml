   M S B A M L   `   `   `  ����L  HBiometria Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Biometria_ProjektBiometria_Projekt  5   	   '    !Biometria_Projekt.HistogramWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Biometria_Projekt     5   	     � Title$  HistogramWindow��5   	   $	��300��6!   $	��300��6.   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��0* �6   �� 5      $��1* �6   .+�5   
   � $��1��6   $��4q�6    � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      .+�5      �� $��0��6   $��0��6'   $
��Gray��64   $��0q�6B   .��5      �� $��1q�6   
���   �
�������$��4q�6T   ._�5      � -   5        $��polygonL��5       �� Stretch$
 Fill=�6Z   
���   � J� Opacity$	 0.8Q�6v    � Points 6.   �� *LuminanceHistogramPoints+6v   5      5      �� 5      $��0��6   $��1��6'   $	��Red��64   $��0q�6A   .��5      �� $��1q�6   
���   �
�������$��4q�6T   ._�5      � -   5        $��polygonR��5      $
 Fill=�6Y   
���  ��$	 0.8Q�6s    6.   �� *RedColorHistogramPoints+6s   5      5      �� 5       $��0��6   $��2��6'   $��Green��64   $��0q�6C   .��5!      �� $��1q�6   
���   �
�������$��4q�6T   ._�5"      � -   5        $��polygonG��5"      $
 Fill=�6[   
��� � �$	 0.8Q�6w    6.   �� *GreenColorHistogramPoints+6w   5#      5$      �� 5%      $��0��6   $��3��6'   $
��Blue��64   $��0q�6B   .��5&      �� $��1q�6   
���   �
�������$��4q�6T   ._�5'      � -   5        $��polygonB��5'      $
 Fill=�6Z   
����  �$	 0.8Q�6u    6.   �� *BlueColorHistogramPoints+6u   5(      5)      5*      5-      5.      