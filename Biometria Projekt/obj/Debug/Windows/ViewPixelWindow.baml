   M S B A M L   `   `   `  ����L  HBiometria Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Biometria_Projekt.WindowsBiometria_Projekt.Windows  5   	   /    )Biometria_Projekt.Windows.ViewPixelWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Biometria_Projekt.Windows     5   	     � Title$  ViewPixelWindow��5   	   $	��250��6!   $	��350��6.   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   6.   �� 5      $
��0.1* �6   6.   �� 5      $
��0.4* �6   6.   �� 5      $
��0.4* �6   6.    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   $��2��6   $��ORYGINALNY OBRAZ��6-   6J   �� 5   
   $��0��6   $��3��6   $��ZMIENIONY OBRAZ��6-   6I   �� 5   
   -   5         ��Name$ label��5      $��X��6   $��1��6+   $��5q�68   �� 5    
   -   5        $ 
label_Copy��5       $��Y��6$   $��2��60   $��5q�6=   �� 5"   
   -   5         ��Name$ CoordinateX��5"      $��1��6'   $��1��64   $��5q�6D   6Q   �� 5#   
   -   5        $ CoordinateY��5#      $��2��6'   $��1��64   $��5q�6D   6Q   �� 5%   
   -   5        $ LabelX��5%      $��15(�6    $��1��6.   $��2��6<   �� 5&   
   -   5        $ LabelY��5&      $��15(�6    $��2��6.   $��2��6<   �� 5'   
   -   5        $ LabelR��5'      $��15(�6    $��3��6.   $��2��6<   �� 5(   
   -   5        $ LabelG��5(      $��15(�6    $��4��6.   $��2��6<   �� 5)   
   -	   5        $ LabelB��5)      $��15(�6!   $��5��6/   $��2��6<   �� 5+   
   -
   5        $ LabelChangeX��5+      $��15(�6&   $��1��64   $��3��6A   �� 5,   
   -   5        $ LabelChangeY��5,      $��15(�6&   $��2��64   $��3��6A   �� 5-   
   -   5        $ LabelChangeR��5-      $��15(�6'   $��3��65   $��3��6B   �� 5.   
   -   5        $ LabelChangeG��5.      $��15(�6'   $��4��65   $��3��6B   �� 5/   
   -   5        $ LabelChangeB��5/      $��15(�6'   $��5��65   $��3��6B   �� 51   
   -   5        $��ButtonConfirm��51      $��6��6&   $��5��63   $��	Zatwierdz��6G   6z   52      53      