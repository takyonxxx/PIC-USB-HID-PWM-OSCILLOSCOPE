//printf("DATA: %f %f\r\n", (double) (adc_read1 * 5.0 / 1023.0),(double) (adc_read2 * 5.0 / 1023.0));
#include <18F4550.h>
#device ADC=10 

//configure a 20MHz crystal to operate at 48MHz (USB High speed)
#fuses NOWDT,NOPROTECT,NOLVP,NODEBUG
#fuses PLL5    // Divide 20MHz crystal to feed 4MHz to the PLL
#fuses HSPLL
#fuses CPUDIV1 // PLL Postscaler divides by 2 ==>  Primary clock = 48 MHz
#fuses USBDIV  // Use clock from PLL
#fuses VREGEN    // Enables 3.3V

#use delay(clock=48Mhz)  // 48 MHzPrimary Clock comes from PLL Postscaler
#use rs232(baud=9600,STOP=1, BITS=8, PARITY=N, ERRORS, UART1)   //baud=5050  
#define USB_HID_DEVICE     TRUE              
#define USB_EP1_TX_ENABLE  USB_ENABLE_BULK    
#define USB_EP1_RX_ENABLE  USB_ENABLE_BULK    
#define USB_EP1_TX_SIZE   128 
#define USB_EP1_RX_SIZE   8    
#include <pic18_usb.h>      
#include "USB_18F4550_CONF.h"             
#include <usb.c>   
#use FAST_IO(A)         // I/O dir doesn't change when
#use FAST_IO(B)         // reading and writing to ports
#use FAST_IO(C)
#use FAST_IO(D)
unsigned int8 datain[8];
unsigned int8 dataout[8];
static unsigned int16 adc_read = 0;
static unsigned int16 pwmfreq = 255;
static unsigned int16 pwmduty = 128;
static unsigned int16 channel = 0;
static unsigned int16 pwmmode = 16;
int32 xtalfreq = 48000000;  
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
void try_usb(void) 
{ 
   printf("USB Connecting\r");  
   usb_init();  
   usb_task();
   printf("Wait_for_enumeration\r");        
   usb_wait_for_enumeration();
   if(usb_enumerated()) 
   printf("USB Connected\r");  
   else
   printf("USB Not Connected\r"); 
} 
void user_init(void) 
{ 
   //disable_interrupts(GLOBAL);  
   set_tris_a(0xff);
   set_tris_b(0xff);
   set_tris_e(0xff);
   set_tris_c(0b10000000);
   setup_CCP1(CCP_PWM);
   setup_adc_ports(ALL_ANALOG);  // A0 A1 A2 A3 A5 E0 E1 E2 B2 B3 B1 B4 B0  
   setup_adc(ADC_CLOCK_DIV_8); 
   setup_ccp1(CCP_PWM);  // Configure CCP1 as a PWM    
   enable_interrupts(GLOBAL);
   printf("PIC18F4550 READY\r");   
   try_usb();
} 
int32 getFreq(int16 pwnfreq, int16 timediv)
{  
   return (int32)xtalfreq /(timediv * (pwnfreq + 1) * 4); 
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
printf(
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f\r" 
,adc1,adc2,adc3,adc4,adc5,adc6,adc7,adc8,adc9,adc10,adc11,adc12);
//puts(gonderilecekveri);
}void usbgonder()
{
sprintf(gonderilecekveri,
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f;" 
"%1.0f;%1.0f;%1.0f;%1.0f" 
,adc1,adc2,adc3,adc4,adc5,adc6,adc7,adc8,adc9,adc10,adc11,adc12);
usb_put_packet(1, gonderilecekveri,128, USB_DTS_TOGGLE);
}

void main(void) 
{   
      user_init(); 
      while(TRUE) 
      {       
       if(kbhit())
       {
        char key = getc(); 
              if(key=='r')
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
                    serigonder();              
              // printf("Data: \%d\r\n", key); 
              } 
              else if(key=='p')
              {
                  x = (unsigned int) pwmduty;
                  setup_timer_2(T2_DIV_BY_16,pwmfreq,1);  
                  set_pwm1_duty(x);                           
                  printf("PWM START\r");     
              }
              else if(key=='1')
              {                 
                  pwmduty=pwmduty-1;
                  if(pwmduty>255)pwmduty=255;
                  if(pwmduty<0)pwmduty=0;                        
                  x = (unsigned int) pwmduty;
                  set_pwm1_duty(x); 
                  printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));   
              }
               else if(key=='2')
              {                 
                  pwmduty=pwmduty+1;
                  if(pwmduty>255)pwmduty=255;
                  if(pwmduty<0)pwmduty=0;                        
                  x = (unsigned int) pwmduty;
                  set_pwm1_duty(x); 
                  printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));   
              }
              else if(key=='3')
              {
                pwmfreq=pwmfreq-1;
                pwmduty=pwmfreq/2;
                if(pwmfreq>255)pwmfreq=255;
                if(pwmfreq<0)pwmfreq=0; 
                if(pwmduty>255)pwmduty=255;
                if(pwmduty<0)pwmduty=0;                        
                  x = (unsigned int) pwmduty;
                  set_pwm1_duty(x); 
                  setup_timer_2(T2_DIV_BY_16,pwmfreq,1);  
                  printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));     
              }
              else if(key=='4')
              {
                pwmfreq=pwmfreq+1;
                pwmduty=pwmfreq/2;
                if(pwmfreq>255)pwmfreq=255;
                if(pwmfreq<0)pwmfreq=0; 
                if(pwmduty>255)pwmduty=255;
                if(pwmduty<0)pwmduty=0;                        
                  x = (unsigned int) pwmduty;
                  set_pwm1_duty(x);     
                  setup_timer_2(T2_DIV_BY_16,pwmfreq,1);  
                  printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));   
              }
              else if(key=='s')
              {
                  set_pwm1_duty(0L);                          
                  printf("PWM STOP\r"); 
              }
              else if(key=='u')
              {
               try_usb();
              }
       }
       if (usb_kbhit(1))             //Eðer pc'den yeni bir paket geldiyse
               {             
                     usb_get_packet(1, datain, 8); //paketi oku    
                    // printf("\r\nPaket1: %d",datain[0]);  
                     switch(datain[0])            
                     {
                        case 'c':
                        {                  
                            printf("PC CONNECTED\r");                             
                            break;
                        }  
                        case 'e':
                        {                  
                            printf("PC DISCONNECTED\r");                             
                            break;
                        }   
                       case 'u':
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
                        case 'p':
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
                           printf("PWM START\r");                          
                           break;
                        }                        
                        case 's':
                        { 
                           set_pwm1_duty(0L);                          
                           printf("PWM STOP\r");                           
                           break;
                        }                                        
                        case 'd':
                        { 
                         pwmduty=datain[1];
                         if(pwmduty>255)pwmduty=255;
                         if(pwmduty<0)pwmduty=0;                        
                         x = (unsigned int) pwmduty;
                         set_pwm1_duty(x); 
                         printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));                            
                           break;
                        }
                        case 'f':
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
                           printf("PWM:%LdDUTY:%LdFREQ:%Ld\r",pwmfreq,pwmduty,getFreq(pwmfreq,pwmmode));                           
                           break;
                        }
                       case 'r':
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
    
                              

  
