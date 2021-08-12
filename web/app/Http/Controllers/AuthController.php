<?php

namespace App\Http\Controllers;

use DB;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Validator;


class AuthController extends Controller
{
    public function __construct(){
        $this->middleware('auth:api', ['except' => ['login', 'register']]);
    }

    public function login(Request $request){
        $validator = Validator::make($request->all(), [
            'use_email' => 'required|email',
            'password' => 'required|string|min:3'
        ]);

        if($validator->fails()){
            return response()->json($validator->errors(),400);

        }

        $token_validity = 24*60;
        $this->guard()->factory()->setTTL($token_validity);

        if(!$token = $this->guard()->attempt($validator->validated())){
            return response()->json(['error' => 'Thông tin đăng nhập không chính xác'], 401);
        }


        return $this->respondWithToken($token);
    }

    public function register(Request $request){
        $validator = Validator::make($request->all(),[
            'use_name' => 'required|string|between:3,50',
            'use_email' => 'required|email|unique:users',
            'password' => 'required|confirmed|min:3',
            'use_phone' => 'required|string|min:3'
        ]);
        if($validator->fails()){
            return response()->json([$validator->errors()], 422);
        }
        $user = User::create(array_merge(
            $validator->validated(),
            ['password' => bcrypt($request->password)]
        ));

        return response()->json(['message'=> 'Tạo người dùng thành công', 'user' => $user]);

    }

    public function logout(){
        $this->guard()->logout();
        return response()->json(['message' => 'Đăng xuất thành công']);
    }



    public function refresh(){
        return $this->respondWithToken($this->guard()->refresh());
    }

    protected function respondWithToken($token){
        return response()->json([
            'token' => $token,
            'token_type' => 'bearer',
            'token_validity' => $this->guard()->factory()->getTTL() * 60
        ]);
    }
    protected function guard(){
        return Auth::guard();
    }
}
