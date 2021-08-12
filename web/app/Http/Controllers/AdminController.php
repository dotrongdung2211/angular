<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Http\Client\Response;
use GuzzleHttp\Client;
use Illuminate\Support\Facades\Validator;


class AdminController extends Controller
{

    public function showlogin(){
        return view('showlogin');
    }

    public function dangky(){
        $response = Http::post('http://127.0.0.1:8000/api/auth/register',[
            "use_name"=>$request->name,
            "use_email"=>$request->email,
            "password"=>$request->password
        ])->json();
        return view('dangky');
    }
    public function check(Request $request){

        $messages = [
            'use_email.required' => 'Email bắt buộc nhập',
            'use_email.email' => 'Không đúng định dạng email',
            'password.required' => 'Mật khẩu bắt buộc nhập',
            'password.min' => 'Mật khẩu phải có ít nhất 3 ký tự',
        ];

        $this->validate($request,[
            'use_email'=>'required|email',
            'password'=>'required|string|min:3'
        ], $messages);

        $response = Http::post('http://127.0.0.1:8000/api/auth/login',[
            "use_email"=>$request->use_email,
            "password"=>$request->password
        ])->json();

        if(isset($response['token'])){
            $request->session()->put('token', $response['token']);
            $request->session()->put('token_type', $response['token_type']);
            return redirect('profile');
        }else{
            return back()->with('fail',$response['error']);
        }



        // $client = new \GuzzleHttp\Client([
        //     'timeout'  => 3.0,
        //     'headers' => ['Accept'=> 'application/json',]
        // ]);
        // //$password =bcrypt($request->password);
        // $res = $client->request('POST', 'http://127.0.0.1:8000/api/auth/login',[
        //     'form_params' => [
        //         'use_email' => $request->use_email,
        //         'password' => $request->password,
        //     ],
        // ]);
        // $response = $res->getBody()->getContents();
        // return $response;


        // $user = User::where('use_email', '=', $request->use_email)->first();
        // if($user){
        //     if(Hash::check($request->password, $user->password)){
        //         $request->session()->put('LoggedUser', $user->id);
        //         return redirect('profile');
        //     }else{
        //         return back()->with('fail','Invail password');
        //     }
        // }else{
        //     return back()->with('fail','No account found for this email');
        // }

    }
    public function profile(){
        return view('profile');
    }


}
