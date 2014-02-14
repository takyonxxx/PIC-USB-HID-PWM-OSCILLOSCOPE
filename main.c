//printf("DATA: %f %f\r\n", (double) (adc_read1 * 5.0 / 1023.0),(double) (adc_read2 * 5.0 / 1023.0));
#include <18F4550.h>
#device ADC=10 
#fuses HSPLL,USBDIV,PLL5,PUT,CPUDIV1,VREGEN,NOWDT,NOPROTECT,NOLVP,NODEBUG,NOMCLR 
#use delay(clock=20000000) 
#use rs232(baud=9600,rcv=pin_c7,xmit=pin_c6) 
#define USB_HID_DEVICE     TRUE              
#define USB_EP1_TX_ENABLE  USB_ENABLE_BULK    
#define USB_EP1_RX_ENABLE  USB_ENABLE_BULK    
#define USB_EP1_TX_SIZE   128 
#define USB_EP1_RX_SIZE   8    
#include <pic18_usb.h>      
#include "USB_18F4550_CONF.h"             
#include <usb.c>   
#use fast_io(a)
#use fast_io(b)
#use fast_io(c)
#use fast_io(d)
unsigned int8 datain[8];
unsigned int8 dataout[8];
static unsigned int16 adc_read = 0;
static unsigned int16 pwmfreq = 255;
static unsigned int16 pwmduty = 128;
static unsigned int16 channel = 0;
static unsigned int16 pwmmode = 16;
unsigned int pwm1;
unsigned int x; 
int i=0,r=0;
int al=1;
float floatveri;
char seri[7],veri;
char serial();
void serigonder();
float adc1,adc2,adc3,adc4,adc5,adc6,adc7,adc8,adc9,adc10,adc11,adc12,adc;
float readadc(int port);
char gonderilecekveri[128];
void user_init(void) 
{ 
   disable_interrupts(GLOBAL);  
   SET_TRIS_A(0xff); 
   SET_TRIS_B(0xff);
   SET_TRIS_E(0xff);  
   setup_adc_ports(ALL_ANALOG);  // A0 A1 A2 A3 A5 E0 E1 E2 B2 B3 B1 B4 B0  
   setup_adc(ADC_CLOCK_DIV_8);    
   output_low(PIN_C1);   // Set CCP1 output low   
   setup_ccp1(CCP_PWM);  // Configure CCP1 as a PWM    
} 
void send_data(unsigned int16 data) 
{ 
 dataout[0]=make8(data,0);
 dataout[1]=make8(data,1);   
 usb_put_packet(1, dataout, 128, USB_DTS_TOGGLE);
}
float readadc(int port)  {
 set_adc_channel(port);
 adc=read_adc(); 
 return adc;
}
void serigonder()
{
sprintf(gonderilecekveri,
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f" 
,adc1,adc2,adc3,adc4,adc5,adc6,adc7,adc8,adc9,adc10,adc11,adc12);
puts(gonderilecekveri);
}void usbgonder()
{
sprintf(gonderilecekveri,
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f" 
,adc1,adc2,adc3,adc4,adc5,adc6,adc7,adc8,adc9,adc10,adc11,adc12);
usb_put_packet(1, gonderilecekveri,128, USB_DTS_TOGGLE);
printf("\r\n USB Data Sent:%s",gonderilecekveri);
}

void main(void) 
{ printf("\r\n USB Connecting");          
   user_init(); 
   usb_init();  
   usb_task();
   printf("\r\n Wait_for_enumeration");        
   usb_wait_for_enumeration();
   if(usb_enumerated()) 
   printf("\r\n USB Connected");
   output_low(PIN_B0);     
   for (;;) 
   {       
      while(usb_enumerated()) 
      {                     
       if (usb_kbhit(1))             //Eðer pc'den yeni bir paket geldiyse
               {             
                     usb_get_packet(1, datain, 8); //paketi oku    
                    // printf("\r\nPaket1: %d",datain[0]);  
                     switch(datain[0])            
                     {
                        case 'C':
                        {                  
                            printf("\r\n PC CONNECTED");                             
                            break;
                        }  
                        case 'E':
                        {                  
                            printf("\r\n PC DISCONNECTED");                             
                            break;
                        }   
                       case 'A':
                        { 
                             adc1=readadc(0);
                             adc2=readadc(1);
                             adc3=readadc(3);
                             adc4=readadc(4);
                             adc5=readadc(5);
                             adc6=readadc(6);
                             adc7=readadc(7);
                             adc8=readadc(8);
                             adc9=readadc(9);
                             adc10=readadc(10);
                             adc11=readadc(11);
                             adc12=readadc(12); 
                             usbgonder();                                                        
                            break;
                        }         
                        case 'P':
                        {                         
                           x = (unsigned int) pwmduty;
                           pwmmode=datain[1];
                           if(pwmmode==1)
                           setup_timer_2(T2_DIV_BY_1,pwmfreq,1);  
                           else if(pwmmode==4)
                           setup_timer_2(T2_DIV_BY_4,pwmfreq,1);                             
                           else if(pwmmode==16)
                           setup_timer_2(T2_DIV_BY_16,pwmfreq,1);  
                           set_pwm1_duty(x);                           
                           printf("\r\n PWM START");                          
                           break;
                        }                        
                        case 'S':
                        { 
                           set_pwm1_duty(0L);                          
                           printf("\r\n PWM STOP");                           
                           break;
                        }                                        
                        case 'D':
                        { 
                         pwmduty=datain[1];
                         if(pwmduty>255)pwmduty=255;
                         if(pwmduty<0)pwmduty=0;                        
                         x = (unsigned int) pwmduty;
                         set_pwm1_duty(x); 
                         printf("\r\n PWM: %Ld  DUTY: %Ld",pwmfreq,pwmduty);                         
                           break;
                        }
                        case 'F':
                        {                           
                         pwmfreq=datain[1];
                         if(pwmfreq>255)pwmfreq=255;
                         if(pwmfreq<0)pwmfreq=0;                        
                         if(pwmmode==1)
                           setup_timer_2(T2_DIV_BY_1,pwmfreq,1);  
                           else if(pwmmode==4)
                           setup_timer_2(T2_DIV_BY_4,pwmfreq,1);                             
                           else if(pwmmode==16)
                           setup_timer_2(T2_DIV_BY_16,pwmfreq,1);  
                           printf("\r\n PWM: %Ld  DUTY: %Ld",pwmfreq,pwmduty);                         
                           break;
                        }
                       case 'R':
                        {                              
                         serigonder();                                   
                         break;
                        }
                        default:
                        {                            
                         break;
                     }                        
                  }                         
               }  
      } 
       
 }

} 
    
                              

                   







