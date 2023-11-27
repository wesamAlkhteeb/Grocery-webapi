
using System.Text;

namespace Grocery.Domain.Helper;

public class GenerateHelper{
    
    public static IEnumerable<char> charactersAvaliable = new List<char>(){
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        '@','$','#','*','&','%','+','-'
    };

    private static Random random ;
    
    static GenerateHelper(){
        random = new Random();
    }

    public static String generateCode(int length){
        StringBuilder password =new StringBuilder();
        for(int i=0 ;i<length ; i++){
            int rand = random.Next(0,charactersAvaliable.Count()-1);
            password.Append(charactersAvaliable.ElementAt(rand));
        }
        return password.ToString();
    }

    public static String generateCode(){
        StringBuilder code =new StringBuilder();
        
        for(int i=0 ;i<6 ; i++){
            int rand = random.Next(0,10);
            code.Append(rand);
        }
        return code.ToString();
    }
}