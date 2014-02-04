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
void user_init(void) 
{ 
   disable_interrupts(GLOBAL);  
   SET_TRIS_A(0xff); 
   SET_TRIS_C(0x00); 
   SET_TRIS_D(0x00); 
   setup_adc_ports(AN0_TO_AN1);                                        
   setup_adc(ADC_CLOCK_DIV_32);    
   output_low(PIN_C1);   // Set CCP1 output low   
   setup_ccp1(CCP_PWM);  // Configure CCP1 as a PWM    
} 
void send_data(unsigned int16 data) 
{ 
 dataout[0]=make8(data,0);
 dataout[1]=make8(data,1);   
 usb_put_packet(1, dataout, 128, USB_DTS_TOGGLE);
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
   //delay_ms(2000);
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
                            channel=datain[1];
                            set_adc_channel(channel);
                            adc_read= read_adc();                            
                            printf("\r\nChnl: %Ld, ADC: %Ld ",channel,adc_read); 
                            send_data(adc_read);
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
                       
                        default:
                        {                            
                         break;
                        }
                     }                    
                    
               }          
      } 
 }
   
} 
    
                              

                   







