   M S B A M L   `   `   `  ����H  DTavernManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TavernManager.VuesTavernManager.Vues  5   	   /    )TavernManager.Vues.FenetreCreationTaverne   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:TavernManager.Vues     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Création d'une taverne��5	   	   $	��482��6(   $	��324��65   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $
��1.5* �6   .+�5   
   (� $��0��6   .��5      �� Nom de la taverne6+   5      (� 5   
   $��1��6   $��10q�6   .��5      �� -   5         ��Name$ TextNom��5      $	��150��6'    �� TextAlignment$ Center=�63   ��6l   �� *Nom+ �� Mode$ TwoWay=�6�   5      (� 5   
   $��2��6   .��5      �� Nombre de clients6+   5       (� 5!   
   $��3��6   $��10q�6   .��5"      �� -   5        $ TextNombreClients��5"      $	��150��61   $ Center=�6=   ��6�   �� *NombreClient+$ TwoWay=�6�   5#      (� 5$   
   $��4��6   .��5%      �� Capacité des tables6-   5&      (� 5'   
   $��5��6   $��10q�6   .��5(      �� -   5        $ TextCapacite��5(      $	��150��6,   $ Center=�68   ��6w   �� *
Capacite+$ TwoWay=�6�   5)      (� 5*   
   $��6��6   .��5+      �� Densité des relations6/   5,      �� 5-   
   -   5         ��Name$ SliderDensite��5-   &   $��10,10,10,20q�6   $��7��6=    � Minimum$ 0Q�6J    � Maximum$	 100Q�6V   	 �� AutoToolTipPlacement$	 TopLeft=�6�   
 �� AutoToolTipPrecision$
 0��6�   $��2��66   � Value 6d   �� *	Densite+$ TwoWay=� ^� ToolTip 6�   ��  �� Path$ Value� �� ElementName$ SliderDensite��6L  (� 5.   
   $��8��6   $��2��6   .��5/      �� Proportion d'amis6+   50      �� 51   
   -   5        $ SliderProportion��51   &   $��10,10,10,20q�6   $��9��6@   $ 0Q�6M   $	 100Q�6Y   $	 TopLeft=�6�   $
 0��6�   $��2��6D   6h   �� *
Proportion+$ TwoWay=� 6�   �� $ Value�$ SliderProportion��6Z  (� 52   
   $��10��6   .��53      �� -   5         ��Name$ BoutonValider��53   ^    ^� ShowOnDisabled$
 True��6    ^� InitialShowDelay$ 0��6:   $	��100��6u   ${�False��6�   .��6�   	Valider6�   54      55      56      